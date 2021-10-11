using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMono : MonoBehaviour
{
    protected Factory Factory;

    protected virtual void Awake()
    {
        Factory = FactoryManager.GetFactory();
    }

    protected virtual void Start()
    {

    }

    public virtual void OnDestroy()
    {
        Factory.ReleaseAllRes();
        Factory = null;
    }
}
