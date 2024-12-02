using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public bool isVerticalLocked;

    // Update is called once per frame
    void Update()
    {
        if (!isVerticalLocked)
            gameObject.transform.LookAt(Camera.main.transform.position);
        else
        {
            gameObject.transform.LookAt(Camera.main.transform.position);

            gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
        }
    }
}
