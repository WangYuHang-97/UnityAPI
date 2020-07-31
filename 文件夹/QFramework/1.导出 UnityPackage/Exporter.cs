using System;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QFramework
{
    /// <summary>
    /// 导出 UnityPackage
    /// </summary>
    public class Exporter
    {
        private static string GeneratePackageName()
        {
            return "QFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }

#if UNITY_EDITOR
        [MenuItem("QFramework/1.导出 UnityPackage %e", false, 1)]
        static void MenuClicked()
        {
            EditorUtil.ExportPackage("Assets/QFramework", GeneratePackageName() + ".unitypackage");
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
        }
#endif
    }
}