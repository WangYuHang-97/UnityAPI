using System;
using UnityEngine;

namespace QFramework
{
    public class NewClass
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/4.Transform API 简化", false, 4)]
#endif
        static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            gameObject.transform.SetLocalPosX(5.0f);
            gameObject.transform.SetLocalPosY(5.0f);
            gameObject.transform.SetLocalPosZ(5.0f);

            gameObject.transform.Identity();

            var parentTrans = new GameObject("ParentTransform").transform;
            var childTrans = new GameObject("ChildTransform").transform;

            parentTrans.AddChild(childTrans);
        }
    }
}
