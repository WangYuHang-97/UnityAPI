using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject go = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//添加物体
        {
            go = ObjectPool.Instance.Spawn("Cube", gameObject.transform);
        }
        else if (Input.GetMouseButtonDown(1))//回收物体
        {
            ObjectPool.Instance.UnSpawn(go,1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))//回收全部对象池中的全部物体
        {
            ObjectPool.Instance.UnSpawnAll();
        }
    }
}
