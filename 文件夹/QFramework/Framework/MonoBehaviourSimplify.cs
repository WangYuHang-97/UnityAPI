using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public abstract partial class MonoBehaviourSimplify : MonoBehaviour
    {
        #region Delay
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
        #endregion

        #region MsgDispatcher
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        class MsgRecord
        {
            private MsgRecord() { }

            static Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
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

        public void UnRegisterMsg(string msgName, Action<object> onMsgReceived)
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

        #endregion
    }
}