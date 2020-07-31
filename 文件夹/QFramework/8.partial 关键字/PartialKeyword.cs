using UnityEngine;
using System.Collections;

namespace QFramework
{
    public partial class TransformSimplify
    {
        /// <summary>
        /// 添加子物体
        /// </summary>
        /// <param name="parentTrans">父物体</param>
        /// <param name="childTrans">子物体</param>
        public static void AddChild(Transform parentTrans,Transform childTrans)
        {
            childTrans.SetParent(parentTrans);
        }
    }

    public partial class GameObjectSimplify
    {
        /// <summary>
        /// GameObject.SetActive
        /// </summary>
        /// <param name="transform"></param>
        public static void Show(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        /// <summary>
        /// !GameObject.SetActive
        /// </summary>
        /// <param name="transform"></param>
        public static void Hide(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public class PartialKeyworkd
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/8.partial 关键字", false, 8)]
#endif
        private static void MenuClicked()
        {
            var parentTrans = new GameObject("ParentTransform").transform;
            var childTrans = new GameObject("ChildTransform").transform;

            TransformSimplify.AddChild(parentTrans, childTrans);

            GameObjectSimplify.Hide(parentTrans);
        }
    }
}