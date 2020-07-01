using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {

    private void Awake()
    {
        gameObject.SetActive(false);
        EventCenter.AddListener<string,string>(EventType.ShowText,Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string,string>(EventType.ShowText, Show);
    }

    private void Show(string str1,string str2)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = str1 +str2;
    }
}
