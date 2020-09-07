using UnityEngine;

namespace QFramework
{
    public class HideExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/7.Hide 脚本", false, 7)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<Hide>();
        }
#endif
    }
}
