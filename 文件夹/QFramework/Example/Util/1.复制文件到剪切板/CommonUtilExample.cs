namespace QFramework
{
    public class CommonUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/1.复制文本到剪切板", false, 1)]
#endif
        private static void MenuClicked()
        {
            CommonUtil.CopyText("要复制的文本");
        }
    }
}