using System.Collections;
using UnityEngine;

namespace QFramework
{
    public class FrameworkExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/2.框架示例", false, 2)]
        private static void MenuClicked()
        {
            MsgDispatcher.UnRegisterAll("Do");
            MsgDispatcher.UnRegisterAll("OK");

            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<FrameworkExample>();
        }
#endif
        private void Awake()
        {
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do", DoSomething);
            RegisterMsg("DO1", _ => { });
            RegisterMsg("DO2", _ => { });
            RegisterMsg("DO3", _ => { });

            RegisterMsg("OK", data =>
            {
                Debug.Log(data);

                UnRegisterMsg("OK");
            });
        }

        private IEnumerator Start()
        {
            MsgDispatcher.Send("Do", "hello");

            yield return new WaitForSeconds(1.0f);

            MsgDispatcher.Send("Do", "hello1");

            SendMsg("OK", "hello");
            SendMsg("OK", "hello");
        }

        void DoSomething(object data)
        {
            // do something
            Debug.LogFormat("Received Do msg:{0}", data);
        }

        protected override void OnBeforeDestroy()
        {

        }
    }
}