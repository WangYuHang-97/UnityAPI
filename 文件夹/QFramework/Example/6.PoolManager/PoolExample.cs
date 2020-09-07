using System;
using UnityEngine;

namespace QFramework
{
    public class PoolExample
    {
        class Fish { }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/6.PoolManager", false, 6)]
#endif
        static void MenuClicked()
        {
            var fishPool = new SimpleObjectPool<Fish>(() => new Fish(), null, 100);

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            var fishOne = fishPool.Allocate();

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            fishPool.Recycle(fishOne);

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            for (var i = 0; i < 10; i++)
            {
                fishPool.Allocate();
            }

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);
        }
    }
}