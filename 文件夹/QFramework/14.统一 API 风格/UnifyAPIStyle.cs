using System;
using UnityEngine;

namespace QFramework
{
    public partial class MonoBehaviourSimplify
    {
        public void SendMsg(string msgName, object data)
        {
            MsgDispatcher.Send(msgName, data);
        }

        public void UnRegisterMsg(string msgName)
        {
            var selectedRecords = mMsgRecorder.FindAll(record => record.Name == msgName);

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                mMsgRecorder.Remove(record);

                record.Recycle();
            });


            selectedRecords.Clear();
        }

        public void UnRegisterMsg(string msgName,Action<object> onMsgReceived)
        {
            var selectedRecords = mMsgRecorder.FindAll(record => record.Name == msgName && record.OnMsgReceived == onMsgReceived);

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                mMsgRecorder.Remove(record);

                record.Recycle();
            });


            selectedRecords.Clear();
        }


    }

    public class UnifyAPIStyle : MonoBehaviourSimplify
    {
        protected override void OnBeforeDestroy()
        {
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/14.统一 API 风格", false, 14)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<UnifyAPIStyle>();
        }
#endif
        private void Awake()
        {
            RegisterMsg("OK", data =>
            {
                Debug.Log(data);

                UnRegisterMsg("OK");
            });
        }

        private void Start()
        {
            SendMsg("OK", "hello");
            SendMsg("OK", "hello");
        }
    }
}