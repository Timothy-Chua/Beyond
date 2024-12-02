using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraIndicator : MonoBehaviour
{
    public GameObject[] indices;

    public void ChangeCam(int currentCam)
    {
        for (int i = 0; i < indices.Length; i++)
        {
            if (currentCam == i)
            {
                indices[i].GetComponent<Image>().sprite = UIManager.instance.activeCam;
                continue;
            }

            indices[i].GetComponent<Image>().sprite = UIManager.instance.deactiveCam;
        }
    }
}
