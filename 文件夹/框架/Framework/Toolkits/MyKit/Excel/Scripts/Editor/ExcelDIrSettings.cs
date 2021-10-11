using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Excel
{
    internal class ExcelDirSettings : ScriptableObject
    {
        private const string ExcelDirSettingsResDir = "Assets/QFramework/Framework/Toolkits/MyKit/Excel/Resources";

        private const string ExcelDirSettingsFile = "ExcelDirSettings";

        private const string ExcelDirSettingsFileExtension = ".asset";

        private static ExcelDirSettings instance;

        [SerializeField]
        public List<string> Dirs;

        public static ExcelDirSettings Instance
        {
            get
            {
                instance = Resources.Load<ExcelDirSettings>(ExcelDirSettingsFile);
                if (instance == null)
                {
                    Directory.CreateDirectory(ExcelDirSettingsResDir);
                    instance = ScriptableObject.CreateInstance<ExcelDirSettings>();

                    string assetPath = Path.Combine(ExcelDirSettingsResDir, ExcelDirSettingsFile);
                    string assetPathWithExtension = Path.ChangeExtension(
                        assetPath, ExcelDirSettingsFileExtension);
                    AssetDatabase.CreateAsset(instance, assetPathWithExtension);
                }
                return instance;
            }
        }

    }
}