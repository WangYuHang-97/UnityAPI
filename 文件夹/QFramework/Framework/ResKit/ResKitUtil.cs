using UnityEngine;

namespace QFramework
{
    public class ResKitUtil
    {
        public static string FullPathForAssetBundle(string assetBundleName)
        {
            return Application.streamingAssetsPath + "/AssetBundles/" + GetPlatformName() + "/" + assetBundleName;
        }


        public static string GetPlatformName()
        {

#if UNITY_EDITOR
            return GetPlatformName(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
#else
            return GetPlatformName(Application.platform);
#endif
        }

#if UNITY_EDITOR
        private static string GetPlatformName(UnityEditor.BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";
                case UnityEditor.BuildTarget.iOS:
                    return "iOS";
                case UnityEditor.BuildTarget.Android:
                    return "Android";
                case UnityEditor.BuildTarget.StandaloneLinux:
                case UnityEditor.BuildTarget.StandaloneLinux64:
                case UnityEditor.BuildTarget.StandaloneLinuxUniversal:
                    return "Linux";
                case UnityEditor.BuildTarget.StandaloneOSX:
                    return "OSX";
                case UnityEditor.BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return null;
            }
        }
#endif

        private static string GetPlatformName(RuntimePlatform runtimePlatform)
        {
            switch (runtimePlatform)
            {
                case RuntimePlatform.WebGLPlayer:
                    return "WebgGL";
                case RuntimePlatform.OSXPlayer:
                    return "OSX";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.Android:
                    return "Android";
                default:
                    return null;
            }
        }
    }
}