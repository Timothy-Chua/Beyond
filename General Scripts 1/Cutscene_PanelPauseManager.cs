using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene_PanelPauseManager : MonoBehaviour
{
    public void _BtnResume()
    {
        UIManager_Cutscene.instance._BtnPlay();
    }

    public void _BtnMenu()
    {
        UIManager_Cutscene.instance._BtnMainMenu();
    }

    public void _BtnSkip()
    {
        UIManager_Cutscene.instance._BtnSkip();
    }
}
