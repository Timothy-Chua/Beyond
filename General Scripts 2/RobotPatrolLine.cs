using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPatrolLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Transform[] pointObj;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        CalculateLine();
    }

    private void FixedUpdate()
    {
        //CalculateLine();
    }

    private void CalculateLine()
    {
        lineRenderer.positionCount = pointObj.Length;

        for (int i = 0; i < pointObj.Length; i++)
        {
            Vector3 position = new Vector3(pointObj[i].position.x, pointObj[i].position.y + 1, pointObj[i].position.z);
            lineRenderer.SetPosition(i, position);
        }
    }
}
