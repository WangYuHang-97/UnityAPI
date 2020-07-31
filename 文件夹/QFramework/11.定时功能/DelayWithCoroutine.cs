using UnityEngine;
using System.Collections;
using System;

namespace QFramework
{
    public partial class MonoBehaviourSimplify
    {
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds,Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
    }

    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/11.定时功能",false,11)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<DelayWithCoroutine>();
        }

        protected override void OnBeforeDestroy()
        {
        }
#endif

        void Start()
        {
            Delay(5.0f, () =>
            {
                Debug.Log(" 5 s 之后");
                Hide();
            });
        }
    }
}