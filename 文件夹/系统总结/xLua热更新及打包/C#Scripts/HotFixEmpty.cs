using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 该方法用于预防可能增加新功能,放置于新物体中加载脚本,该方法都是空方法,因此不需要LuaCallCSharp
/// </summary>
[Hotfix()]
public class HotFixEmpty : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {

    }

    private void BehaviourMethod()
    {

    }
}
