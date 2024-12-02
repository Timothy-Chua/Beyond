using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InteractionPanel : MonoBehaviour
{
    private RectTransform movingObj;
    private RectTransform basisObj;
    private Vector3 finalPos;
    private Vector3 pos;

    private void Start()
    {
        movingObj = GetComponent<RectTransform>();
        basisObj = UIManager.instance.panelGame.GetComponent<RectTransform>();
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        pos = Input.mousePosition + UIManager.instance.panelInteractionOffset;

        finalPos.x = Mathf.Lerp(movingObj.position.x, pos.x, Time.deltaTime * 5f);
        finalPos.y = Mathf.Lerp(movingObj.position.y, pos.y, Time.deltaTime * 5f);
        finalPos.z = basisObj.position.z;
        
        movingObj.position = finalPos;
    }
}
