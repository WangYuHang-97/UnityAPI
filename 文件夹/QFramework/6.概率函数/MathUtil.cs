using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// 概率函数
    /// </summary>
    public partial class MathUtil
    {
#if UNITY_EDITOR
        [MenuItem("QFramework/6.概率函数",false,6)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.Percent(50));
        }

        public static bool Percent(int percent)
        {
            return UnityEngine.Random.Range(0, 100) < percent;
        }
    }
}