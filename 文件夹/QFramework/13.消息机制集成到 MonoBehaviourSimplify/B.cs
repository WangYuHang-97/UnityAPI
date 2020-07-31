using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public abstract partial class MonoBehaviourSimplify
    {
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        class MsgRecord
        {
            private MsgRecord(){}

            static Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName,Action<object> onMsgReceived)
            {
                var retRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();
                retRecord.Name = msgName;
                retRecord.OnMsgReceived = onMsgReceived;

                return retRecord;
            }

            public void Recycle()
            {
                Name = null;

                OnMsgReceived = null;

                mMsgRecordPool.Push(this);
            }

            public string Name;

            public Action<object> OnMsgReceived;
        }

        public void RegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            MsgDispatcher.Register(msgName, onMsgReceived);
            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }

        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgRecord in mMsgRecorder)
            {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }

        protected abstract void OnBeforeDestroy();
    }

    public class B : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/13.消息机制集成到 MonoBehaviourSimplify", false, 13)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<B>();
        }
#endif
        private void Awake()
        {
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do", DoSomething);
            RegisterMsg("DO1", _ => { });
            RegisterMsg("DO2", _ => { });
            RegisterMsg("DO3", _ => { });
        }

        private IEnumerator Start()
        {
            MsgDispatcher.Send("Do", "hello");

            yield return new WaitForSeconds(1.0f);

            MsgDispatcher.Send("Do", "hello1");
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