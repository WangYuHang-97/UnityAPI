using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Object = UnityEngine.Object;

public class Factory
{
    private Transform _pool;

    public ResLoader ResLoader = ResLoader.Allocate();

    public Factory(Transform pool)
    {
        _pool = pool;
    }

    public T GetManager<T>() where T : Object
    {
        switch (MyText.Language)
        {
            case Language.TraditionalChinese:
                return ResLoader.LoadSync<T>($"{typeof(T).Name}TCN");
            case Language.SimplifiedChinese:
                return ResLoader.LoadSync<T>($"{typeof(T).Name}SCN");
        }
        return default;
    }

    public Sprite GetSprite(string spriteStr)
    {
        if (spriteStr.Contains("Property"))
        {
            var sprite = ResLoader.LoadSync<Sprite>(spriteStr);
            if (sprite == null) return null;
            return sprite;
        }
        return ResLoader.LoadSync<Sprite>(spriteStr);
    }

    public bool IsEmptySprite(Sprite sprite)
    {
        return sprite.name == GetSprite("image_empty").name;
    }

    public GameObject GetPrefabSync(string prefabStr, Transform trans = null)
    {
        return ResLoader.LoadSync<GameObject>(prefabStr);
    }

    public GameObject GetPrefabSync<T>(Transform trans) where T : MonoBehaviour
    {
        return GetPrefabSync(typeof(T).Name.ToLower(), trans);
    }

    public void PreLoad<T>(int count) where T : MonoBehaviour
    {
        List<T> t = new List<T>();
        for (int i = 0; i < count; i++)
        {
            t.Add(GetPrefabSync<T>(_pool).GetComponent<T>());
        }
        for (int i = 0; i < t.Count; i++)
        {
            Recycle(t[i].gameObject);
        }
    }

    public T GetPrefabScriptSync<T>(Transform trans) where T : MonoBehaviour
    {
        return GetPrefabSync<T>(trans).GetComponent<T>();
    }

    public T GetResource<T>(string name) where T : Object
    {
        return ResLoader.LoadSync<T>(name);
    }

    public object GetResource(string name)
    {
        return ResLoader.LoadSync(name);
    }

    public bool Recycle(GameObject go)
    {
        try
        {
            go.transform.SetParent(_pool);
            go.SetActive(false);
            //if (_recycleTimeList.ContainsKey(go)) //自动回收系统
            //{
            //    _recycleTimeList[go] = TimerSystem.GetTime();
            //}
            //else
            //{
            //    _recycleTimeList.Add(go, TimerSystem.GetTime());
            //}
            //TimerSystem.Intance.AddTimeTask(() =>
            //{
            //    if (_recycleTimeList.ContainsKey(go))
            //    {
            //        var nowTime = TimerSystem.GetTime();
            //        if (_recycleTimeList[go] < nowTime && go.transform.parent == _pool)
            //        {
            //            _recycleTimeList.Remove(go);
            //            PoolAssetSearch[poolName.ToLower()].Destory(go);
            //        }
            //    }
            //}, 5 * 60); //5分钟自动销毁
            return true;
        }
        catch (Exception e)
        {
            Log.E(e);
            Log.E(go.name);
            return false;
        }
    }

    public bool Recycle(Transform go)
    {
        return Recycle(go.gameObject);
    }

    public void RecycleAllChild(Transform parent, int startIndex = 0)
    {
        List<Transform> childList = new List<Transform>();
        for (int i = startIndex; i < parent.childCount; i++)
        {
            childList.Add(parent.GetChild(i));
        }
        foreach (var child in childList)
        {
            if (!Recycle(child))
            {
                Debug.LogError(child + "回收失败");
            }
        }
        if (parent.childCount > startIndex)
        {
            Debug.LogError(parent + "尚未回收所有子物体");
        }
    }

    public void RecycleAllChild(List<Transform> parents)
    {
        foreach (var parent in parents)
        {
            RecycleAllChild(parent);
        }
    }

    public void ReleaseAllRes()
    {
        this.ResLoader.ReleaseAllRes();
    }
}
