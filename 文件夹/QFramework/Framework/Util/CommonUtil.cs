using UnityEngine;

namespace QFramework
{
    public partial class CommonUtil
    {
        public static void CopyText(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }
    }
}