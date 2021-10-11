using System;
using System.Collections.Generic;
using Excel;
using QFramework;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// 工厂管理类，用于资源获取
/// </summary>
public static class FactoryManager
{
    public static ResLoader _resLoader = ResLoader.Allocate();
    private static Transform _pool;

    public static void Init(Transform pool)
    {
        _pool = pool;
    }

    public static Factory GetFactory()
    {
        return new Factory(_pool);
    }

}
