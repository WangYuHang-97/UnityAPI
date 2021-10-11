using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyScrollRectEditor 
{
    [MenuItem("GameObject/OvrrideUI/MyScrollRect", false, 8)]
    public static void AddMyScrollRect()
    {
        GameObject obj = Resources.Load<GameObject>("MS_");
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
