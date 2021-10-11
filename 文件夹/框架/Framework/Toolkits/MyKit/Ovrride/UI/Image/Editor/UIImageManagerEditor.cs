using System;
using System.Collections;
using System.Collections.Generic;
using Excel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIImageManagerEditor 
{
    [MenuItem("CustomEditor/更新繁简体图片")]
    public static void RefreshImage()
    {
        string[] sdirs = { "Assets/Art/UIPrefab", "Assets/Editor Default Resources" };
        var asstIds = AssetDatabase.FindAssets("t:Prefab", sdirs);
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var asstId in asstIds)
        {
            string path = AssetDatabase.GUIDToAssetPath(asstId);
            var pfb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            gameObjects.Add(pfb);
        }
        int i = 0;
        List<UIImage> images = new List<UIImage>();
        foreach (var pfb in gameObjects)
        {
            bool exist = false;
            foreach (var image in pfb.GetComponentsInChildren<MyImage>(true))
            {
                images.Add(new UIImage() { Index = i, Name = image.name, SpriteName = image.SpriteName });
                image.Index = i;
                i++;
                exist = true;
            }
            if (exist)
            {
                try
                {
                    PrefabUtility.SavePrefabAsset(pfb);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
        }

        //excel文本

        UIImageManager imageManager = new UIImageManager
        {
            Images = images
        };
        AssetDatabase.CreateAsset(imageManager, $"{ExcelConfig.assetPath}UIImageManager.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("GameObject/OvrrideUI/MyImage", false,8)]
    public static void AddMyImage()
    {
        GameObject obj = Resources.Load<GameObject>("Img_");
        if (Selection.transforms.Length >0)
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
