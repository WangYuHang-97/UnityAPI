using UnityEngine;
using System.Collections;
using System;

namespace QFramework
{
    /// <summary>
    /// 从若干个值中随机取出一个值
    /// </summary>
    public partial class MathUtil
    {
        /// <summary>
        /// 返回随机数
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="values">数组</param>
        /// <returns></returns>
        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/9.从若干个值中随机取出一个值", false, 9)]
#endif
        static void MenuClicked1()
        {
            var randomAge = GetRandomValueFrom(new int[] { 1, 2, 3 });
            Debug.Log(randomAge);
        }
    }
}