using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuOverlay : MonoBehaviour
{
    private RectTransform movingObj;
    private RectTransform basisObj;
    private Vector3 finalPos;
    private Vector3 pos;

    private void Start()
    {
        movingObj = GetComponent<RectTransform>();
        basisObj = MainMenuManager.instance.panelMain.GetComponent<RectTransform>();
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        pos = Input.mousePosition + MainMenuManager.instance.panelOffset;

        finalPos.x = Mathf.Lerp(movingObj.position.x, pos.x, Time.deltaTime * 5f);
        finalPos.y = Mathf.Lerp(movingObj.position.y, pos.y, Time.deltaTime * 5f);
        finalPos.z = basisObj.position.z;

        movingObj.position = finalPos;
    }
}
