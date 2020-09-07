using System;
using UnityEngine;

namespace QFramework
{
    public class MathUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/5.概率函数 和 随机函数", false, 5)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.Percent(50));

            var randomAge = MathUtil.GetRandomValueFrom(new int[] { 1, 2, 3 });
            Debug.Log(randomAge);
        }

    }
}