using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// 通用方法
    /// </summary>
    public partial class CommonUtil
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/2.复制文本到剪切板", false, 2)]
#endif
        private static void MenuClicked()
        {
            CommonUtil.CopyText("要复制的文本");
        }

        public static void CopyText(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }
    }
}