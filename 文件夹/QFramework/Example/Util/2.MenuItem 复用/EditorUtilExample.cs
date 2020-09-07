using System;

namespace QFramework
{
    public class EditorUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/2.MenuItem 复用", false, 2)]
        private static void MenuClicked()
        {
            EditorUtil.CallMenuItem("QFramework/Example/1.复制文本到剪切板");
        }
#endif
    }
}
