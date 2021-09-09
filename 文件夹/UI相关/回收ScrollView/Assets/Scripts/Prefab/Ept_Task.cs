using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ept_Task : MonoBehaviour
{
    public Text Text_Index;
    public Text Text_Content;

    public void Init(string content,int index)
    {
        Text_Content.text = content;
        Text_Index.text = index.ToString();
    }
}
