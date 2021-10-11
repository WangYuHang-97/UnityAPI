using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MyImage : Image
{
    protected Factory Factory;

    [SerializeField] public int Index;
    [SerializeField] public List<string> SpriteName;

    public static UIImageManager ImageManager;
    public static Language Language;

    protected override void Start()
    {
        base.Start();
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        if (SpriteName.Count!=0)
        {
            Factory = FactoryManager.GetFactory();
            if (ImageManager == null)
            {
                ImageManager = Factory.GetResource<UIImageManager>("UIImageManager");
            }
            ChangeSprite();
        }
    }

    public void ChangeSprite(int index = 0)
    {
        UIImage uiImage = ImageManager.Images[Index];
        switch (Language)
        {
            case Language.TraditionalChinese:
                this.sprite = Factory.GetSprite($"{uiImage.SpriteName[index]}_TN");
                break;
            case Language.SimplifiedChinese:
                this.sprite = Factory.GetSprite($"{uiImage.SpriteName[index]}_CN");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDisable();
        Factory?.ReleaseAllRes();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MyImage), true)]
[CanEditMultipleObjects]
public class MyImageEditor : UnityEditor.UI.ImageEditor
{
    SerializedProperty Index;
    SerializedProperty SpriteName;

    protected override void OnEnable()
    {
        base.OnEnable();
        Index = serializedObject.FindProperty("Index");
        SpriteName = serializedObject.FindProperty("SpriteName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(Index);
        EditorGUILayout.PropertyField(SpriteName);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif