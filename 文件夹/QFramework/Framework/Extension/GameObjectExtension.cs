using UnityEngine;

namespace QFramework
{
    public static partial class GameObjectExtension
    {
        public static void Show(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Hide(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static void Show(this Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        public static void Hide(this Transform transform)
        {
            transform.gameObject.SetActive(false);
        }

        public static void Show(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(true);
        }

        public static void Hide(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
        }
    }
}