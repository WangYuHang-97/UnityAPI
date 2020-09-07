using UnityEngine;

namespace QFramework
{
    public class GameObjectSimplifyExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/6.GameObject API 简化", false, 6)]
#endif
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            gameObject.Hide();

            gameObject.transform.Show();
        }
    }
}