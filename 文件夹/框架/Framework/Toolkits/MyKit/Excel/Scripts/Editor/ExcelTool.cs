using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Excel;
using LitJson;
using NPinyin;
using QFramework;
using UnityEngine;
#if UNITY_EDITOR
    

namespace Excel
{
    public class ExcelTool
    {
        private static bool _scn;
        private static LanguageTranslate _languageTranslate;

        public ExcelTool(bool scn)
        {
            _scn = scn;
            _languageTranslate = new LanguageTranslate();
        }

        static T[] CreatePropertyArrayWithExcelBase<T>(int columnNum, int rowNum, DataRowCollection collect, ref string search) where T : new()
        {
            Dictionary<string, int> searchdic = new Dictionary<string, int>();
            //获得表数据
            //根据excel的定义，第二行开始才是数据
            T[] array = new T[rowNum - 2];
            Dictionary<string, int> properties = new Dictionary<string, int>();
            for (int i = 0; i < columnNum; i++)
            {
                var str = collect[0][i].ToString();
                str = str.Trim();
                if (str != string.Empty)
                {
                    properties.Add(str, i);
                }
            }
            for (int i = 2; i < rowNum; i++)
            {

                T item = new T();
                foreach (var singleProperty in properties)
                {
                    PropertyInfo property = typeof(T).GetProperty(singleProperty.Key);
                    if (property != null)
                    {
                        property.SetValue(item,
                            DataEncryptManager.StringEncoder(TextTrans(collect[i][singleProperty.Value].ToString())));
                    }
                    else
                    {
                        Debug.LogError($"类型{typeof(T).ToString()}没有值：{singleProperty.Key}");
                    }
                }
                //解析每列的数据
                array[i - 2] = item;
                searchdic.Add(DataEncryptManager.StringEncoder(collect[i][0].ToString()), i - 2);
            }
            search = JsonMapper.ToJson(searchdic);
            return array;
        }

        public T[] CreatePropertyArrayWithExcel<T>(IProperty property) where T : new()
        {
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(property.TableDir(), ref columnNum, ref rowNum, property.TableName());
            //根据excel的定义，第二行开始才是数据
            return CreatePropertyArrayWithExcelBase<T>(columnNum, rowNum, collect, ref property.Search);
        }

        public static T[] CreateProperty<T>(IProperty property) where T : new()
        {
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(property.TableDir(), ref columnNum, ref rowNum, property.TableName());
            //根据excel的定义，第二行开始才是数据
            return CreatePropertyArrayWithExcelBase<T>(columnNum, rowNum, collect, ref property.Search);
        }

        public PropertyConst[] CreatePropertyConstArrayWithExcel(string filePath, ref string search)
        {
            List<string> keys = new List<string>();
            Dictionary<string, int> searchdic = new Dictionary<string, int>();
            //获得表数据
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum, 0);
            //根据excel的定义，第二行开始才是数据
            PropertyConst[] array = new PropertyConst[rowNum - 2];
            Dictionary<string, int> properties = new Dictionary<string, int>();
            for (int i = 0; i < columnNum; i++)
            {
                if (collect[0][i].ToString() != string.Empty)
                {
                    properties.Add(collect[0][i].ToString(), i);
                }
            }
            for (int i = 2; i < rowNum; i++)
            {
                keys.Add(collect[i][0] + "," + collect[i][1]);
                PropertyConst item = new PropertyConst();
                foreach (var singleProperty in properties)
                {
                    PropertyInfo property = typeof(PropertyConst).GetProperty(singleProperty.Key);
                    if (property != null)
                    {
                        property.SetValue(item,
                            DataEncryptManager.StringEncoder(TextTrans(collect[i][singleProperty.Value].ToString())));
                    }
                    else
                    {
                        Debug.LogError(singleProperty.Key);
                    }
                }
                //解析每列的数据
                searchdic.Add(DataEncryptManager.StringEncoder(collect[i][0].ToString()), i - 2);
                array[i - 2] = item;
            }
            search = JsonMapper.ToJson(searchdic);
            CreateEnum("ConstType", keys);
            return array;
        }

        public PropertyExcelText[] CreatePropertyExcelTextArrayWithExcel(PropertyExcelTextManager textManager)
        {
            List<string> keys = new List<string>();
            Dictionary<string, int> searchdic = new Dictionary<string, int>();
            //获得表数据
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(textManager.TableDir(), ref columnNum, ref rowNum, 1);
            //根据excel的定义，第二行开始才是数据
            PropertyExcelText[] array = new PropertyExcelText[rowNum - 2];
            Dictionary<string, int> properties = new Dictionary<string, int>();
            for (int i = 0; i < columnNum; i++)
            {
                if (collect[0][i].ToString() != string.Empty)
                {
                    properties.Add(collect[0][i].ToString(), i);
                }
            }
            for (int i = 2; i < rowNum; i++)
            {
                keys.Add(collect[i][0] + "," + collect[i][1]);
                PropertyExcelText item = new PropertyExcelText();
                foreach (var singleProperty in properties)
                {
                    PropertyInfo property = typeof(PropertyExcelText).GetProperty(singleProperty.Key);
                    if (property != null)
                    {
                        property.SetValue(item,
                            DataEncryptManager.StringEncoder(TextTrans(collect[i][singleProperty.Value].ToString())));
                    }
                    else
                    {
                        Debug.LogError(singleProperty.Key);
                    }
                }
                //解析每列的数据
                searchdic.Add(DataEncryptManager.StringEncoder(collect[i][0].ToString()), i - 2);
                array[i - 2] = item;
            }
            textManager.Search = JsonMapper.ToJson(searchdic);
            CreateEnum("ExcelTextType", keys);
            return array;
        }

        public PropertyProperty[] CreatePropertyPropertyArrayWithExcel(string filePath, PropertyPropertyManager iProperty)
        {
            List<string> keys = new List<string>();
            Dictionary<string, int> searchdic = new Dictionary<string, int>();
            Dictionary<string, int> CNSearchdic = new Dictionary<string, int>();
            //获得表数据
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum, iProperty.TableName());
            //根据excel的定义，第二行开始才是数据
            PropertyProperty[] array = new PropertyProperty[rowNum - 2];
            Dictionary<string, int> properties = new Dictionary<string, int>();
            for (int i = 0; i < columnNum; i++)
            {
                if (collect[0][i].ToString() != string.Empty)
                {
                    properties.Add(collect[0][i].ToString(), i);
                }
            }
            for (int i = 2; i < rowNum; i++)
            {
                PropertyProperty item = new PropertyProperty();
                foreach (var singleProperty in properties)
                {
                    PropertyInfo property = typeof(PropertyProperty).GetProperty(singleProperty.Key);
                    if (property != null)
                    {
                        property.SetValue(item,
                            DataEncryptManager.StringEncoder(TextTrans(collect[i][singleProperty.Value].ToString())));
                    }
                    else
                    {
                        Debug.LogError(singleProperty.Key);
                    }
                }
                //解析每列的数据
                searchdic.Add(DataEncryptManager.StringEncoder(collect[i][0].ToString()), i - 2);
                if (!string.IsNullOrEmpty(collect[i][1].ToString()))
                {
                    var str = Split(Pinyin.GetPinyin(collect[i][1].ToString()));
                    CNSearchdic.Add(str, i - 2);
                    keys.Add(str);
                }
                array[i - 2] = item;
            }
            iProperty.Search = JsonMapper.ToJson(searchdic);
            iProperty.CNSearch = JsonMapper.ToJson(CNSearchdic);
            CreatePropertyEnum("PropertyPropertyType", keys);
            return array;
        }

        static string Split(string pinYin)
        {
            var retStr = "";
            var strs = pinYin.Split(' ');
            foreach (var str in strs)
            {
                if (int.TryParse(str, out var num))
                {
                    retStr += num.ToString();
                }
                else
                {
                    retStr += str.Substring(0, 1).ToUpper() + str.Substring(1);
                }
            }
            return retStr;
        }

        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNum">行数</param>
        /// <param name="rowNum">列数</param>
        /// <returns></returns>
        static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum, int sheetNum = 0)
        {

            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            return ReadExcelBase(result.Tables[sheetNum], ref columnNum, ref rowNum);
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

        void CreateEnum(string enumName, List<string> fields)
        {
            //准备一个代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();
            //设置命名空间（这个是指要生成的类的空间）
            CodeNamespace myNamespace = new CodeNamespace("PeacefulMartialClub");
            //Code:代码体
            CodeTypeDeclaration myClass = new CodeTypeDeclaration(enumName);
            myClass.IsEnum = true;
            //设置类的访问类型
            myClass.TypeAttributes = TypeAttributes.Public;// | TypeAttributes.Sealed;
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);

            foreach (var fieldStr in fields)
            {
                //添加字段
                var str = fieldStr.Split(',');
                CodeMemberField field = new CodeMemberField(typeof(System.String), str[0]);
                field.Attributes = MemberAttributes.Public;
                field.Comments.Add(new CodeCommentStatement(str[1]));
                myClass.Members.Add(field);
            }

            //生成C#脚本("VisualBasic"：VB脚本)
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //代码风格:大括号的样式{}
            options.BracingStyle = "C";
            //是否在字段、属性、方法之间添加空白行
            options.BlankLinesBetweenMembers = true;
            //输出文件路径
            string outputFile = Application.dataPath + "/Scripts/PeacefulMartialClub/Scripts/Type/" + enumName + ".cs";
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            //保存
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
        }

        void CreatePropertyEnum(string enumName, List<string> fields)
        {
            //准备一个代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();
            //设置命名空间（这个是指要生成的类的空间）
            CodeNamespace myNamespace = new CodeNamespace("PeacefulMartialClub");
            //Code:代码体
            CodeTypeDeclaration myClass = new CodeTypeDeclaration(enumName);
            myClass.IsEnum = true;
            //设置类的访问类型
            myClass.TypeAttributes = TypeAttributes.Public;// | TypeAttributes.Sealed;
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);

            foreach (var fieldStr in fields)
            {
                if (string.IsNullOrEmpty(fieldStr)) continue;
                //添加字段
                CodeMemberField field = new CodeMemberField(typeof(System.String), fieldStr);
                field.Attributes = MemberAttributes.Public;
                myClass.Members.Add(field);
            }

            //生成C#脚本("VisualBasic"：VB脚本)
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //代码风格:大括号的样式{}
            options.BracingStyle = "C";
            //是否在字段、属性、方法之间添加空白行
            options.BlankLinesBetweenMembers = true;
            //输出文件路径
            string outputFile = Application.dataPath + "/Scripts/PeacefulMartialClub/Scripts/Data/" + enumName + ".cs";
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            //保存
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
        }

        static bool HasChinese(string str)
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

        static string TextTrans(string str)
        {
            if (HasChinese(str))
            {
                if (_scn)
                {
                    return _languageTranslate.ConvertChinSimp(str);
                }
                else
                {
                    return _languageTranslate.ConvertChinTrad(str);
                }
            }
            return str;
        }
    }
}
#endif
