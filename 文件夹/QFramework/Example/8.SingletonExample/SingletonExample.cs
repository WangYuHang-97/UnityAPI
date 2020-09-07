using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class SingletonExample : Singleton<SingletonExample>
    {
        private SingletonExample()
        {
            Debug.Log("singleton example ctor");
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/8.SingletonExample", false, 8)]
#endif
        static void MenuClicked()
        {
            var initInstance = SingletonExample.Instance;
            initInstance = SingletonExample.Instance;
        }
    }
}