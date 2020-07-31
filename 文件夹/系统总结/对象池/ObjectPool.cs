using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get => _instance;
    }

    public string ResourceDir = "";//资源目录
    public Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();//对象池大全


    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// 从对象池中取出游戏物体
    /// </summary>
    /// <param name="name">对象池名称,即预制体名称</param>
    /// <param name="parentTransform">将要储存的父物体</param>
    /// <returns></returns>
    public GameObject Spawn(string name, Transform parentTransform)
    {
        SubPool CurrentPool = null;//单个对象池初始化
        if (!m_pools.ContainsKey(name))//如果对象池大全里不包括该name对象池，则调用RegieterNew方法生成一个新对象池
        {
            RegieterNew(name, parentTransform);
        }
        CurrentPool = m_pools[name];//对象池大全里包括该name对象池，直接调用
        return CurrentPool.Spawn();//返回对象池基类中取出方法(返回游戏物体)
    }

    /// <summary>
    /// 将物体回收至该物体类型的对象池中
    /// </summary>
    /// <param name="go">需要回收的物体</param>
    /// <param name="time">回收单个物体次数</param>
    public void UnSpawn(GameObject go,int time = 1)
    {
        SubPool CurrentPool = null;//单个对象池初始化
        foreach (var pool in m_pools.Values)//遍历所有对象池大全的值，如果某对象池包含该游戏物体，赋值给CurrentPool
        {
            if (pool.Contain(go))
            {
                CurrentPool = pool;
                break;
            }
        }
        CurrentPool.UnSpawn(go,time);//使用对象池基类中回收方法
    }
    /// <summary>
    /// 将所有物体回收至该物体类型的对象池中
    /// </summary>
    public void UnSpawnAll()
    {
        foreach (var pool in m_pools.Values)//对所有对象池使用UnSpawnAll方法，回收所有物体
        {
            pool.UnSpawnAll();
        }
    }
    /// <summary>
    /// 新建一个新对象池
    /// </summary>
    /// <param name="name">该物体名称</param>
    /// <param name="ParentTransform">将要挂载的父物体</param>
    void RegieterNew(string name, Transform ParentTransform)
    {
        string path = ResourceDir + "/" + name;//资源路径
        GameObject go = Resources.Load<GameObject>(path);//通过Resource导入资源
        SubPool pool = new SubPool(ParentTransform, go);//生成新对象池
        m_pools.Add(pool.Name, pool);//加入对象池大全
    }

    /// <summary>
    /// 清除对象池
    /// </summary>
    public void Clear()
    {
        m_pools.Clear();
    }
}
