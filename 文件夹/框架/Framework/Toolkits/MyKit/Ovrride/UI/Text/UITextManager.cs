using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextManager : ScriptableObject
{
    public List<UIText> FixedText;
    public List<UIText> ExcelText;
}

[System.Serializable()]
public class UIText
{
    [SerializeField] public string ParentName;
    [SerializeField] public string Name;
    [SerializeField] public bool Fixed;
    [SerializeField] public int Index;
    [SerializeField] public List<string> SimplifiedChineses;
    [SerializeField] public List<string> TraditionalChineses;
}
