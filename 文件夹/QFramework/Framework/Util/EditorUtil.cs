#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace QFramework
{
    public partial class EditorUtil
    {
        public static void OpenInFolder(string folderPath)
        {
            Application.OpenURL("file:///" + folderPath);
        }

#if UNITY_EDITOR
        public static void ExportPackage(string assetPathName, string fileName)
        {
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
        }

        public static void CallMenuItem(string menuName)
        {
            EditorApplication.ExecuteMenuItem(menuName);
        }
#endif
    }
}