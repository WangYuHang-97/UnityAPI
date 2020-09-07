using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace QFramework
{
    public class Exporter
    {
        private static string GeneratePackageName()
        {
            return "QFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }

        [MenuItem("QFramework/Framework/Editor/导出 UnityPackage %e", false, 1)]
        static void MenuClicked()
        {
            EditorUtil.ExportPackage("Assets/QFramework", GeneratePackageName() + ".unitypackage");
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
        }
    }
}