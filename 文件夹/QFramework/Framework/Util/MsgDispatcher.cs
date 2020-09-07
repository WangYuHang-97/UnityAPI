using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class MsgDispatcher : MonoBehaviour
    {

        static Dictionary<string, Action<object>> mRegisteredMsgs = new Dictionary<string, Action<object>>();

        public static void Register(string msgName, Action<object> onMsgReceived)
        {
            if (!mRegisteredMsgs.ContainsKey(msgName))
            {
                mRegisteredMsgs.Add(msgName, _ => { });
            }

            mRegisteredMsgs[msgName] += onMsgReceived;
        }

        public static void UnRegisterAll(string msgName)
        {
            mRegisteredMsgs.Remove(msgName);
        }

        public static void UnRegister(string msgName, Action<object> onMsgReceived)
        {
            if (mRegisteredMsgs.ContainsKey(msgName))
            {
                mRegisteredMsgs[msgName] -= onMsgReceived;
            }
        }

        public static void Send(string msgName, object data)
        {
            if (mRegisteredMsgs.ContainsKey(msgName))
            {
                mRegisteredMsgs[msgName](data);
            }
        }
    }
}