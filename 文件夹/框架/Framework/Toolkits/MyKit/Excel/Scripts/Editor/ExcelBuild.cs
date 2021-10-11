using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Excel;
using QFramework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Excel
{
#if UNITY_EDITOR

    public class ExcelBuild
    {
        private List<Type> _ignoreList;
        private List<Object> _ignoreProperties;
        private List<Object> _properties;
        private string _outputFile;

        private bool _scn;

        [MenuItem("CustomEditor/CreateAsset")]
        public static void CreateItemAsset()
        {
            IgnoreList(out var scnProperies, true);
            new ExcelBuild(scnProperies, Application.dataPath + "/Plugins/Excel/Scripts/Type/", true);

            IgnoreList(out var tcnProperties, false);
            new ExcelBuild(tcnProperties, Application.dataPath + "/Plugins/Excel/Scripts/Type/", false);
        }

        /// <summary>
        /// 忽略名单
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        static void IgnoreList(out List<Object> properties, bool isSim)
        {
            properties = new List<Object>();
            ExcelTool excelTool = new ExcelTool(isSim);
            PropertyConstManager propertyConstManager = ScriptableObject.CreateInstance<PropertyConstManager>();
            properties.Add(propertyConstManager);
            propertyConstManager.PropertyConst = excelTool.CreatePropertyConstArrayWithExcel(ExcelConfig.PropertyConst, ref propertyConstManager.Search);

            PropertyPropertyManager propertyPropertyManager = ScriptableObject.CreateInstance<PropertyPropertyManager>();
            properties.Add(propertyPropertyManager);
            propertyPropertyManager.PropertyProperty = excelTool.CreatePropertyPropertyArrayWithExcel(ExcelConfig.PropertyCH2 + ExcelConfig.PropertyFight, propertyPropertyManager);
        }

        public ExcelBuild(List<Object> ignoreProperties, string outputFile,bool scn)
        {
            _ignoreProperties = ignoreProperties;
            _outputFile = outputFile;
            _scn = scn;
            _ignoreList = new List<Type>();
            foreach (var property in ignoreProperties)
            {
                _ignoreList.Add(property.GetType());
            }
            _properties = new List<Object>();
            CreateItemAssetBase();
        }
        
        void CreateItemAssetBase()
        {
            DirectoryInfo root = new DirectoryInfo(_outputFile);
            foreach (var file in root.GetFiles())
            {
                if (!file.Name.Contains("meta") && file.Name[0] != 'I')
                {
                    string name = file.Name.Replace(".cs", "");
                    Type type = GetTypyByName(name);
                    bool ignoreType = false;
                    foreach (var ignore in _ignoreList)
                    {
                        if (ignore == type)
                        {
                            ignoreType = true;
                            break;
                        }
                    }
                    if (!ignoreType) _properties.Add(ScriptableObject.CreateInstance(type));
                }
            }
            ExcelTool excelTool = new ExcelTool(_scn);
            for (int i = 0; i < _properties.Count; i++)
            {
                try
                {
                    var manager = _properties[i].As<IProperty>();
                    var name = manager.GetType().Name.Replace("Manager", "");
                    var type = GetTypyByName(name);
                    MethodInfo mi = excelTool.GetType().GetMethod("CreateProperty")
                        .MakeGenericMethod(type);
                    var items = mi.Invoke(null, new object[] {manager});
                    manager.Split((Item[]) items);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Debug.Log(_properties[i].GetType().Name);
                    throw;
                }
            }
            foreach (var property in _ignoreProperties)
            {
                _properties.Add(property);
            }
            string dir = "";
            dir = _scn ? ExcelConfig.assetPathSCN : ExcelConfig.assetPathTCN;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (DeleteAllFile(dir)) ;
            foreach (var property in _properties)
            {
                string assetPath = _scn?$"{dir}{property}SCN.asset": $"{dir}{property}TCN.asset";
                AssetDatabase.CreateAsset(property, assetPath);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("输出文件成功");
        }

        /// <summary>
        /// 通过名字查询Type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        Type GetTypyByName(string typeName)
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

        /// <summary>
        /// 删除指定文件目录下的所有文件
        /// </summary>
        /// <param name="fullPath">文件路径</param>
        bool DeleteAllFile(string fullPath)
        {
            //获取指定路径下面的所有资源文件  然后进行删除
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo direction = new DirectoryInfo(fullPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.EndsWith(".meta"))
                    {
                        continue;
                    }
                    string FilePath = fullPath + "/" + files[i].Name;
                    File.Delete(FilePath);
                }
                return true;
            }
            return false;
        }
    }
#endif

}