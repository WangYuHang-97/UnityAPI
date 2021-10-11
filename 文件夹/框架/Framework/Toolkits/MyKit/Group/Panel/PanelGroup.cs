using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class PanelGroup : UIElement
{
    [Header("Mark")] public PanelGroupMark PanelGroupMark;
    [Header("对应标题（可为空）")]public GameObject Text_Title;
    [Header("对应标题按钮")] public Button Button;

    protected Canvas Canvas => GetComponent<Canvas>();

    public override string ComponentName { get; }

    internal void OpenPanel()
    {
        if (PanelGroupMark != null)
        {
            PanelGroupMark.CurrentPanel?.ClosePanel();
            PanelGroupMark.CurrentPanel = this;
        }
        if (Canvas == null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Canvas.enabled = true;
        }
        Button.interactable = false;
        Text_Title?.gameObject.SetActive(true);
        OnOpen();
    }

    protected virtual void OnOpen()
    {

    }

    internal void ClosePanel()
    {
        if (Canvas == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Canvas.enabled = false;
        }
        Button.interactable = true;
        Text_Title?.gameObject.SetActive(false);
        OnClose();
    }

    protected virtual void OnClose()
    {

    }


    public virtual void Recycle()
    {

    }
}
