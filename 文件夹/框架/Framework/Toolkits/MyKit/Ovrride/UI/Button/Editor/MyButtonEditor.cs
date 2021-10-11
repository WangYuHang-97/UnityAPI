using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyButtonEditor
{
    [MenuItem("GameObject/OvrrideUI/MyButton", false, 8)]
    public static void AddMyButton()
    {
        GameObject obj = Resources.Load<GameObject>("Btn_");
        if (Selection.transforms.Length > 0)
        {
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject newObj = GameObject.Instantiate(obj);
                newObj.transform.SetParent(Selection.transforms[i]);
                newObj.name = obj.name + i;
            }
        }
        else
        {
            GameObject newObj = GameObject.Instantiate(obj);
            newObj.transform.SetParent(null);
        }
    }

    [MenuItem("GameObject/OvrrideUI/MyButton(Text)", false, 8)]
    public static void AddMyButtonWithText()
    {
        GameObject obj = Resources.Load<GameObject>("Btn_(WithText)");
        if (Selection.transforms.Length > 0)
        {
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject newObj = GameObject.Instantiate(obj);
                newObj.transform.SetParent(Selection.transforms[i]);
                newObj.name = obj.name + i;
            }
        }
        else
        {
            GameObject newObj = GameObject.Instantiate(obj);
            newObj.transform.SetParent(null);
        }
    }
}
