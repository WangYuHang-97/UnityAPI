#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// 屏幕宽高比判断
    /// </summary>
    public partial class ResolutionCheck
    {
#if UNITY_EDITOR
        [MenuItem("QFramework/4.屏幕宽高比判断", false, 4)]
#endif
        static void MenuClicked()
        {
            Debug.Log(IsPadResolution() ? "是 Pad" : "不是 Pad");
            Debug.Log(IsPhoneResolution() ? "是 Phone" : "不是 Phone");
            Debug.Log(IsiPhoneXResolution() ? "是 iphonex" : "不是 iphonex");
        }

        public static float GetAspectRatio()
        {
            var isLandscape = Screen.width > Screen.height;
            return isLandscape ? (float)Screen.width / Screen.height : (float)Screen.height / Screen.width;
        }

        public static bool IsPadResolution()
        {
            return InAspectRange(4.0f / 3);
        }

        public static bool IsPhoneResolution()
        {
            return InAspectRange(16.0f / 9);
        }

        public static bool IsiPhoneXResolution()
        {
            return InAspectRange(2436.0f / 1125);
        }

        public static bool InAspectRange(float dstAspectRatio)
        {
            var aspect = GetAspectRatio();
            return aspect > (dstAspectRatio - 0.05) && aspect < (dstAspectRatio + 0.05);
        }
    }
}