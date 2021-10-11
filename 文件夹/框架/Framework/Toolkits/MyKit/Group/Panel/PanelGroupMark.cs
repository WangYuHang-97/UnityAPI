using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroupMark : MonoBehaviour
{
    public PanelGroup CurrentPanel;

    public void CloseAll()
    {
        CurrentPanel?.ClosePanel();
        CurrentPanel = null;
    }
}
