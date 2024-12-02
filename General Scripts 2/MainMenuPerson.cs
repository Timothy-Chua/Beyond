using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainMenuInfo))]
public class MainMenuPerson : MonoBehaviour
{
    private MainMenuInfo m_MainMenuInfo;

    private void Awake()
    {
        m_MainMenuInfo = GetComponent<MainMenuInfo>();
    }

    private void OnMouseEnter()
    {
        SetInfoText();
    }

    private void OnMouseExit()
    {
        ClearInfoText();
    }

    public void SetInfoText()
    {
        MainMenuManager.instance.txtInfo.text = m_MainMenuInfo.info;
    }

    public void ClearInfoText()
    {
        MainMenuManager.instance.txtInfo.text = null;
    }
}
