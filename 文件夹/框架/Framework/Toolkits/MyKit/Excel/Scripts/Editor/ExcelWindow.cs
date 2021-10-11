using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR


namespace Excel
{
    public class ExcelWindow : EditorWindow
    {
        private string _propertyName = "Property";
        private string _excelName = "";
        private string _excelDir;

        public int Index = 0;

        [MenuItem("CustomEditor/CreateC#")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            ExcelWindow window = (ExcelWindow)EditorWindow.GetWindow(typeof(ExcelWindow));
        }

        void OnGUI()
        {
            GUILayout.Label("编译器脚本生成", EditorStyles.boldLabel);
            _propertyName = EditorGUILayout.TextField("Property名字", _propertyName);
            _excelName = EditorGUILayout.TextField("Excel表名", _excelName);
            Index = EditorGUILayout.Popup(Index,ExcelDirSettings.Instance.Dirs.ToArray());

            _excelDir = EditorGUILayout.TextField("其他表地址", _excelDir);
            if (GUILayout.Button("生成item") && !string.IsNullOrEmpty(_propertyName) && !string.IsNullOrEmpty(_excelName)) MakeItem();
            if (GUILayout.Button("生成Manager") && !string.IsNullOrEmpty(_propertyName) && !string.IsNullOrEmpty(_excelName)) MakeManager();
        }

        void MakeItem()
        {
            if (string.IsNullOrEmpty(_excelDir))
            {
                _excelDir = ExcelDirSettings.Instance.Dirs.ToArray()[Index];
            }

            //item生成
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace myNamespace = new CodeNamespace("Excel");
            CodeTypeDeclaration myClass = new CodeTypeDeclaration(_propertyName)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public,
            };
            myClass.CustomAttributes.Add(new CodeAttributeDeclaration("System.Serializable"));//类的Attributes
            myClass.BaseTypes.Add(typeof(Item));
            //设置类的访问类型
            // | TypeAttributes.Sealed;
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);

            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(_excelDir + ".xlsx", ref columnNum, ref rowNum, _excelName);
            List<string> properties = new List<string>();
            List<string> propertiesWithChinese = new List<string>();
            List<string> ints = new List<string>();
            List<string> floats = new List<string>();
            for (int i = 0; i < columnNum; i++)
            {
                var propertyStr = collect[0][i].ToString();
                propertyStr = propertyStr.Trim();
                if (propertyStr != string.Empty)
                {
                    properties.Add(propertyStr);
                    var str = collect[2][i].ToString();
                    if (str != string.Empty)
                    {
                        if (int.TryParse(str, out int y))
                        {
                            ints.Add(propertyStr);
                        }
                        else if (float.TryParse(str, out float x))
                        {
                            floats.Add(propertyStr);
                        }
                        else if (HasChinese(str))
                        {
                            propertiesWithChinese.Add(propertyStr);
                            properties.Remove(propertyStr);
                        }
                    }
                }
            }

            foreach (var property in ints)
            {
                CodeMemberMethod method = new CodeMemberMethod();
                method.Name = "Get" + property;
                method.Attributes = MemberAttributes.Public;
                method.ReturnType = new CodeTypeReference("System.Int32");
                var fieldStr = FirstCharToUpper(property);
                method.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"DataEncryptManager.StringToIntDecoder({fieldStr})")));
                myClass.Members.Add(method);
            }

            foreach (var property in floats)
            {
                CodeMemberMethod method = new CodeMemberMethod();
                method.Name = "Get" + property;
                method.Attributes = MemberAttributes.Public;
                method.ReturnType = new CodeTypeReference("System.Single");
                var fieldStr = FirstCharToUpper(property);
                method.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"DataEncryptManager.StringToFloatDecoder({fieldStr})")));
                myClass.Members.Add(method);
            }

            //添加字段
            foreach (var fieldStr in properties)
            {
                CodeMemberField field = new CodeMemberField(typeof(System.String), FirstCharToUpper(fieldStr));
                field.Attributes = MemberAttributes.Private;
                field.CustomAttributes.Add(new CodeAttributeDeclaration("UnityEngine.SerializeField"));//字段的Attributes
                myClass.Members.Add(field);
            }
            foreach (var fieldStr in propertiesWithChinese)
            {
                CodeMemberField field = new CodeMemberField(typeof(System.String), FirstCharToUpper(fieldStr));
                field.Attributes = MemberAttributes.Private;
                field.CustomAttributes.Add(new CodeAttributeDeclaration("UnityEngine.SerializeField"));//字段的Attributes
                myClass.Members.Add(field);
            }

            //添加属性
            foreach (var fieldStr in properties)
            {
                CodeMemberProperty property1 = new CodeMemberProperty();
                property1.Name = fieldStr;
                property1.Type = new CodeTypeReference("System.String");
                property1.Attributes = MemberAttributes.Public;
                property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FirstCharToUpper(fieldStr))));
                property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FirstCharToUpper(fieldStr)), new CodePropertySetValueReferenceExpression()));
                myClass.Members.Add(property1);
            }
            foreach (var fieldStr in propertiesWithChinese)//添加属性
            {
                CodeMemberProperty property1 = new CodeMemberProperty();
                property1.Name = fieldStr;
                property1.Type = new CodeTypeReference("System.String");
                property1.Attributes = MemberAttributes.Public;
                property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($" DataEncryptManager.StringDecoder({FirstCharToUpper(fieldStr)})")));
                property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FirstCharToUpper(fieldStr)), new CodePropertySetValueReferenceExpression()));
                myClass.Members.Add(property1);
            }

            //生成C#脚本("VisualBasic"：VB脚本)
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //代码风格:大括号的样式{}
            options.BracingStyle = "C";
            //是否在字段、属性、方法之间添加空白行
            options.BlankLinesBetweenMembers = true;
            //输出文件路径
            
            string outputFile = Application.dataPath + "/Plugins/Excel/Scripts/Item/" + _propertyName + ".cs";
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            //保存
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }

            Debug.Log("文件"+_propertyName+"Item生成成功");
        }

        public static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum, string sheetNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();

            DataTable dataTable = null;
            try
            {
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    if (sheetNum == result.Tables[i].TableName)
                    {
                        dataTable = result.Tables[i];
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(filePath);
                throw;
            }
            return ReadExcelBase(dataTable, ref columnNum, ref rowNum);
        }

        static DataRowCollection ReadExcelBase(DataTable dataTable, ref int columnNum, ref int rowNum)
        {
            columnNum = dataTable.Columns.Count;
            rowNum = dataTable.Rows.Count;

            for (int i = 0; i < rowNum; i++)
            {
                if (dataTable.Rows[i][0].ToString() == string.Empty)
                {
                    rowNum = i;
                    break;
                }
            }
            for (int i = 0; i < columnNum; i++)
            {
                if (dataTable.Rows[1][i].ToString() == string.Empty)
                {
                    columnNum = i;
                    break;
                }
            }
            return dataTable.Rows;
        }

        bool HasChinese(string str)
        {
            bool isChinese(char c)
            {
                return c >= 0x4E00 && c <= 0x9FA5;
            }

            char[] ch = str.ToCharArray();
            if (str != null)
            {
                for (int i = 0; i < ch.Length; i++)
                {
                    if (isChinese(ch[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void MakeManager()
        {
            //item生成
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace myNamespace = new CodeNamespace("Excel");
            CodeTypeDeclaration myClass = new CodeTypeDeclaration(_propertyName+"Manager")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public,
            };
            myClass.BaseTypes.Add(typeof(IProperty));
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            myNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);
            Type type = typen(_propertyName);
            if (type == null)
            {
                Debug.LogError($"生成失败，请等待{_propertyName}编译完成");
                return;
            }
            type = type.MakeArrayType();


            bool hasPerent = false;
            foreach (var member in typen(_propertyName).GetProperties())
            {
                if (member.Name == "Percent")
                {
                    hasPerent = true;
                    break;
                }
            }

            CodeMemberField field = new CodeMemberField(type, FirstCharToUpper(_propertyName));
            field.Attributes = MemberAttributes.Public;
            myClass.Members.Add(field);


            CodeMemberProperty property1 = new CodeMemberProperty();
            property1.Name = _propertyName;
            property1.Type = new CodeTypeReference(type);
            property1.Attributes = MemberAttributes.Public;
            property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FirstCharToUpper(_propertyName))));
            property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FirstCharToUpper(_propertyName)), new CodePropertySetValueReferenceExpression()));
            myClass.Members.Add(property1);

            CodeMemberMethod method1 = new CodeMemberMethod();//表名
            method1.Name = "TableName";
            method1.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            method1.ReturnType = new CodeTypeReference("System.String");
            method1.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"\"{_excelName}\"")));
            myClass.Members.Add(method1);

            CodeMemberMethod method2 = new CodeMemberMethod();//split
            method2.Name = "Split";
            method2.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            method2.Parameters.Add(new CodeParameterDeclarationExpression("Excel.Item[]", "objs"));
            method2.Statements.Add(new CodeSnippetStatement($"base.Split(objs);"));
            method2.Statements.Add(new CodeSnippetStatement($"{_propertyName} = ({type})objs;"));
            myClass.Members.Add(method2);

            CodeMemberMethod method4 = new CodeMemberMethod();//split
            method4.Name = "TableDir";
            method4.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            method4.ReturnType = new CodeTypeReference("System.String");
            method4.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"\"{_excelDir.Replace("\\","//")+".xlsx"}\"")));
            myClass.Members.Add(method4);

            if (hasPerent)
            {
                CodeMemberMethod method3 = new CodeMemberMethod();//split
                method3.Name = "GetPro";
                method3.Attributes = MemberAttributes.Public;
                method3.Parameters.Add(new CodeParameterDeclarationExpression($"out List<{typen(_propertyName)}>", $"{FirstCharToUpper(_propertyName)}"));
                method3.Parameters.Add(new CodeParameterDeclarationExpression($"out List<string>", $"pros"));
                method3.Statements.Add(new CodeSnippetStatement($"{FirstCharToUpper(_propertyName)} = new List<{typen(_propertyName)}>();"));
                method3.Statements.Add(new CodeSnippetStatement($"pros = new List<string>();"));
                method3.Statements.Add(new CodeSnippetStatement($"foreach (var obj in {_propertyName})"));
                method3.Statements.Add(new CodeSnippetStatement("{"));
                method3.Statements.Add(new CodeSnippetStatement($"{FirstCharToUpper(_propertyName)}.Add(obj);"));
                method3.Statements.Add(new CodeSnippetStatement($"pros.Add(obj.Percent);"));
                method3.Statements.Add(new CodeSnippetStatement("}"));
                myClass.Members.Add(method3);
            }

            //生成C#脚本("VisualBasic"：VB脚本)
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //代码风格:大括号的样式{}
            options.BracingStyle = "C";
            //是否在字段、属性、方法之间添加空白行
            options.BlankLinesBetweenMembers = true;
            //输出文件路径
            string outputFile = Application.dataPath + "/Plugins/Excel/Scripts/Type/" + _propertyName+"Manager" + ".cs";
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            //保存
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }

            Debug.Log("文件" + _propertyName + "Manager生成成功");
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return input[0].ToString().ToLower()+input.Substring(1);
        }

        private Type typen(string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int i = 0; i < assemblyArrayLength; ++i)
            {
                type = assemblyArray[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            for (int i = 0; (i < assemblyArrayLength); ++i)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }
            return type;
        }
    }
}
#endif
