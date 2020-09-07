using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class ResolutionCheckExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/3.屏幕宽高比判断", false, 3)]
#endif
        static void MenuClicked()
        {
            Debug.Log(ResolutionCheck.IsPadResolution() ? "是 Pad" : "不是 Pad");
            Debug.Log(ResolutionCheck.IsPhoneResolution() ? "是 Phone" : "不是 Phone");
            Debug.Log(ResolutionCheck.IsPhone15Resolution() ? "是 4s" : "不是 4s");
            Debug.Log(ResolutionCheck.IsiPhoneXResolution() ? "是 iphonex" : "不是 iphonex");
        }
    }
}