using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageManager : ScriptableObject
{
    public List<UIImage> Images;
}

[System.Serializable()]
public class UIImage
{
    [SerializeField] public string Name;
    [SerializeField] public int Index;
    [SerializeField] public List<string> SpriteName;
}
