using System;
using System.Collections;
using System.Collections.Generic;
using Excel;
using QFramework;
using UnityEditor;
using UnityEngine;

public class UITextManagerEditor 
{

    [MenuItem("CustomEditor/更新繁简体文本文件")]
    public static void UpdateText()
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
        var manager = AssetDatabase.LoadAssetAtPath<UITextManager>($"{ExcelConfig.assetPath}UITextManager.asset");
        Dictionary<int, MyText> search = new Dictionary<int, MyText>();
        int i = manager.FixedText.Count;
        List<UIText> texts = manager.FixedText;
        foreach (var pfb in gameObjects)
        {
            bool needRebuild = false;
            foreach (var text in pfb.GetComponentsInChildren<MyText>(true))
            {
                if (!search.ContainsKey(text.Index))
                {
                    search.Add(text.Index, text);
                    UIText uiText = texts[text.Index];
                    RefreshText(text, ref uiText);
                    texts[text.Index] = uiText;
                }
                else
                {
                    needRebuild = true;
                    bool exist = false;
                    GameObject go = null;
                    foreach (var mono in pfb.GetComponentsInChildren<MonoBehaviour>(true))
                    {
                        foreach (var field in mono.GetType().GetFields())
                        {
                            var xx = mono.GetFieldByReflect(field.Name);
                            if (xx.IsNotNull() && xx.GetType() == typeof(MyText))
                            {
                                if (xx.As<MyText>() == text)
                                {
                                    go = mono.gameObject;
                                    exist = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!exist && text.GetComponent("TextAdapt") == null)
                    {
                        texts.Add(GetText(true, i, pfb.name, text));
                    }
                    else
                    {
                        texts.Add(GetText(false, i, go?.gameObject.name, text));
                    }
                    text.Index = i;
                    i++;
                }
            }
            try
            {
                if (needRebuild) PrefabUtility.SavePrefabAsset(pfb);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        //excel文本

        ExcelTool excelTool = new ExcelTool(true);
        PropertyExcelTextManager propertyExcelTextManager = ScriptableObject.CreateInstance<PropertyExcelTextManager>();
        propertyExcelTextManager.PropertyExcelText = excelTool.CreatePropertyExcelTextArrayWithExcel(propertyExcelTextManager);
        List<UIText> excelTexts = new List<UIText>();
        LanguageTranslate translate = new LanguageTranslate();
        for (int j = 0; j < propertyExcelTextManager.PropertyExcelText.Length; j++)
        {
            excelTexts.Add(new UIText()
            {
                Fixed = true,
                Index = j,
                Name = DataEncryptManager.StringDecoder(propertyExcelTextManager.PropertyExcelText[j].ConstKey),
                ParentName = "Excel",
                SimplifiedChineses = new List<string>() { translate.ConvertChinSimp(propertyExcelTextManager.PropertyExcelText[j].ConstValue) },
                TraditionalChineses = new List<string>() { translate.ConvertChinTrad(propertyExcelTextManager.PropertyExcelText[j].ConstValue) },
            });
        }
        UITextManager textManager = new UITextManager
        {
            FixedText = texts,
            ExcelText = excelTexts
        };
        AssetDatabase.CreateAsset(textManager, $"{ExcelConfig.assetPath}UITextManager.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("CustomEditor/重洗繁简体文本文件(慎用！！！，时间长)")]
    public static void RefreshText()
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
        List<UIText> texts = new List<UIText>();
        foreach (var pfb in gameObjects)
        {
            foreach (var text in pfb.GetComponentsInChildren<MyText>(true))
            {
                bool exist = false;
                GameObject go = null;
                foreach (var mono in pfb.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    foreach (var field in mono.GetType().GetFields())
                    {
                        var xx = mono.GetFieldByReflect(field.Name);
                        if (xx.IsNotNull() && xx.GetType() == typeof(MyText))
                        {
                            if (xx.As<MyText>() == text)
                            {
                                go = mono.gameObject;
                                exist = true;
                                break;
                            }
                        }
                    }
                }
                if (!exist && text.GetComponent("TextAdapt") == null)
                {
                    texts.Add(GetText(true, i, pfb.name, text));
                }
                else
                {
                    texts.Add(GetText(false, i, go?.gameObject.name, text));
                }
                text.Index = i;
                i++;
            }
            try
            {
                PrefabUtility.SavePrefabAsset(pfb);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        //excel文本

        ExcelTool excelTool = new ExcelTool(true);
        PropertyExcelTextManager propertyExcelTextManager = ScriptableObject.CreateInstance<PropertyExcelTextManager>();
        propertyExcelTextManager.PropertyExcelText = excelTool.CreatePropertyExcelTextArrayWithExcel(propertyExcelTextManager);
        List<UIText> excelTexts = new List<UIText>();
        LanguageTranslate translate = new LanguageTranslate();
        for (int j = 0; j < propertyExcelTextManager.PropertyExcelText.Length; j++)
        {
            excelTexts.Add(new UIText()
            {
                Fixed = true,
                Index = j,
                Name = DataEncryptManager.StringDecoder(propertyExcelTextManager.PropertyExcelText[j].ConstKey),
                ParentName = "Excel",
                SimplifiedChineses = new List<string>() { translate.ConvertChinSimp(propertyExcelTextManager.PropertyExcelText[j].ConstValue) },
                TraditionalChineses = new List<string>() { translate.ConvertChinTrad(propertyExcelTextManager.PropertyExcelText[j].ConstValue) },
            });
        }
        UITextManager textManager = new UITextManager
        {
            FixedText = texts,
            ExcelText = excelTexts
        };
        AssetDatabase.CreateAsset(textManager, $"{ExcelConfig.assetPath}UITextManager.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static UIText GetText(bool fix, int index, string parentName, MyText text)
    {
        LanguageTranslate translate = new LanguageTranslate();
        UIText uiText = new UIText()
        {
            Fixed = fix,
            Index = index,
            Name = text.gameObject.name,
            ParentName = parentName,
            SimplifiedChineses = new List<string>(),
            TraditionalChineses = new List<string>(),
        };
        if (text.SimplifiedChinese == null || text.SimplifiedChinese.Count == 0)
        {
            string str = text.text.Replace("｛", "{").Replace("｝", "}");
            uiText.SimplifiedChineses.Add(translate.ConvertChinSimp(str));
            uiText.TraditionalChineses.Add(translate.ConvertChinTrad(str));
        }
        else
        {
            foreach (var chinese in text.SimplifiedChinese)
            {
                string str = chinese.Replace("｛", "{").Replace("｝", "}");
                uiText.SimplifiedChineses.Add(translate.ConvertChinSimp(str));
                uiText.TraditionalChineses.Add(translate.ConvertChinTrad(str));
            }
        }
        return uiText;
    }

    static void RefreshText(MyText text, ref UIText uiText)
    {
        LanguageTranslate translate = new LanguageTranslate();
        uiText.SimplifiedChineses = new List<string>();
        uiText.TraditionalChineses = new List<string>();
        if (text.SimplifiedChinese == null || text.SimplifiedChinese.Count == 0)
        {
            string str = text.text.Replace("｛", "{").Replace("｝", "}");
            uiText.SimplifiedChineses.Add(translate.ConvertChinSimp(str));
            uiText.TraditionalChineses.Add(translate.ConvertChinTrad(str));
        }
        else
        {
            foreach (var chinese in text.SimplifiedChinese)
            {
                string str = chinese.Replace("｛", "{").Replace("｝", "}");
                uiText.SimplifiedChineses.Add(translate.ConvertChinSimp(str));
                uiText.TraditionalChineses.Add(translate.ConvertChinTrad(str));
            }
        }
    }

    [MenuItem("GameObject/OvrrideUI/MyText", false, 8)]
    public static void AddMyText()
    {
        GameObject obj = Resources.Load<GameObject>("Text_");
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
