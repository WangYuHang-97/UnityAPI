#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// Transform API 简化
    /// </summary>
    public partial class TransformSimplify
    {
#if UNITY_EDITOR
        [MenuItem("QFramework/5.Transform API 简化", false, 5)]
#endif
        static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            SetLocalPosX(gameObject.transform, 5.0f);
            SetLocalPosY(gameObject.transform, 5.0f);
            SetLocalPosZ(gameObject.transform, 5.0f);

            Identity(gameObject.transform);
        }

        public static void Identity(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public static void SetLocalPosX(Transform transform, float x)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosY(Transform transform, float y)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosZ(Transform transform, float z)
        {
            var localPos = transform.localPosition;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXY(Transform transform, float x, float y)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXZ(Transform transform, float x, float z)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosYZ(Transform transform, float y, float z)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            transform.localPosition = transform.localPosition;
        }
    }
}