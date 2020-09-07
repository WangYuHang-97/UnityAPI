using UnityEngine;

namespace QFramework
{
    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/1.定时功能", false, 1)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<DelayWithCoroutine>();
        }
#endif
        
        protected override void OnBeforeDestroy()
        {
        }

        void Start()
        {
            Delay(5.0f, () =>
            {
                Debug.Log(" 5 s 之后");
                this.Hide();
            });
        }
    }
}