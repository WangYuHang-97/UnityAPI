using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MyText : Text
{
    protected Factory Factory;

    [SerializeField] public int Index;
    [SerializeField] public List<string> SimplifiedChinese;
    public static UITextManager TextManager;
    public static Language Language;

    protected override void Start()
    {
        base.Start();
#if UNITY_EDITOR
        if (!Application.isPlaying)return;
#endif
        Factory = FactoryManager.GetFactory();
        if (TextManager == null)
        {
            TextManager = Factory.GetResource<UITextManager>("UITextManager");
        }
        UIText uiText = TextManager.FixedText[Index];
        if (uiText.TraditionalChineses.Count == 1 && !uiText.TraditionalChineses[0].Contains("{0}"))
        {
            switch (Language)
            {
                case Language.TraditionalChinese:
                    this.text = uiText.TraditionalChineses[0];
                    break;
                case Language.SimplifiedChinese:
                    this.text = uiText.SimplifiedChineses[0];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void Text(int index = 0)
    {
        Text(new object[] { "" }, index);
    }

    public void Text(string content, int index = 0)
    {
        Text(new object[] { content }, index);
    }

    public void Text(object[] args, int index = 0)
    {
        UIText uiText = TextManager.FixedText[Index];
        if (uiText.TraditionalChineses.Count <= index)
        {
            switch (Language)
            {
                case Language.TraditionalChinese:
                    this.text = String.Format(uiText.TraditionalChineses[0], index);
                    break;
                case Language.SimplifiedChinese:
                    this.text = String.Format(uiText.SimplifiedChineses[0], index);
                    break;
            }
        }
        else
        {
            switch (Language)
            {
                case Language.TraditionalChinese:
                    this.text = String.Format(uiText.TraditionalChineses[index], args);
                    break;
                case Language.SimplifiedChinese:
                    this.text = String.Format(uiText.SimplifiedChineses[index], args);
                    break;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDisable();
        Factory?.ReleaseAllRes();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MyText), true)]
[CanEditMultipleObjects]
public class MyTextEditor : UnityEditor.UI.TextEditor
{
    SerializedProperty Index;
    SerializedProperty SimplifiedChinese;

    protected override void OnEnable()
    {
        base.OnEnable();
        Index = serializedObject.FindProperty("Index");
        SimplifiedChinese = serializedObject.FindProperty("SimplifiedChinese");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(Index);
        EditorGUILayout.PropertyField(SimplifiedChinese);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
