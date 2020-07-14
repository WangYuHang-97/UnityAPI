using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个对象池基类
/// </summary>
public class SubPool
{
    //某物体集合
    List<GameObject> m_objects = new List<GameObject>();

    //预设
    private GameObject m_prefab;//预制体
    private Transform m_parent;//将要储存的父物体

    /// <summary>
    /// 返回预制体名字
    /// </summary>
    public string Name
    {
        get { return m_prefab.name; }
    }

    /// <summary>
    /// 预设物体初始化
    /// </summary>
    /// <param name="parent">挂载父物体对象</param>
    /// <param name="go">预制体</param>
    public SubPool(Transform parentTransform, GameObject go)
    {
        m_prefab = go;//m_prefab赋值
        m_parent = parentTransform;//m_parent赋值
    }
    /// <summary>
    /// 取出物体方法至ObjectPool类(返回游戏物体GameObject)
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        GameObject go = null;//游戏物体初始化
        foreach (var obj in m_objects)//在物体集合中遍历是否有游戏物体处于激活状态
        {
            if (!obj.activeSelf)
            {
                go = obj;
            }
        }
        if (go == null)//物体集合中为空，创建新的游戏物体
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);//实例化
            go.transform.parent = m_parent;//设定将要储存的父物体
            m_objects.Add(go);//加入至游戏物体集合中
        }
        go.SetActive(true);//设置为激活状态
        return go;
    }
    /// <summary>
    /// 回收单个物体方法至ObjectPool类
    /// </summary>
    /// <param name="go">需要回收的物体</param>
    /// <param name="time">回收单个物体次数</param>
    public void UnSpawn(GameObject go,int time=1)
    {
        int Spawntime = time;
        foreach (var obj in m_objects)
        {
            if (obj.name == go.name && obj.activeSelf)
            {
                obj.SetActive(false);
                Spawntime--;
                if (Spawntime == 0)
                {
                    break;;
                }
            }
        }
    }
    /// <summary>
    /// 回收全部物体方法至ObjectPool类（多次调用UnSpawn方法）
    /// </summary>
    public void UnSpawnAll()
    {
        foreach (var obj in m_objects)//寻找到激活物体并使用UnSpawn方法
        {
            if (obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }
    /// <summary>
    /// 物体集合中是否包含该游戏物体
    /// </summary>
    /// <param name="go">游戏物体</param>
    /// <returns></returns>
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
