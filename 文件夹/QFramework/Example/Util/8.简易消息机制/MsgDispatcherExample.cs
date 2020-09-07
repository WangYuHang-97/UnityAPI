using UnityEngine;

namespace QFramework
{
    public class MsgDispatcherExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/8.简易消息机制", false, 8)]
#endif
        private static void MenuClicked()
        {
            MsgDispatcher.UnRegisterAll("消息1");

            MsgDispatcher.Register("消息1", OnMsgReceived);
            MsgDispatcher.Register("消息1", OnMsgReceived);

            MsgDispatcher.Send("消息1", "hello world");

            MsgDispatcher.UnRegister("消息1", OnMsgReceived);

            MsgDispatcher.Send("消息1", "hello");
        }

        static void OnMsgReceived(object data)
        {
            Debug.LogFormat("消息1:{0}", data);
        }
    }
}
