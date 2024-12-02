using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpecifiedFloor : MonoBehaviour
{
    public int playerIndex;
    public int camGroupIndex;
    public int assignedCam;
    public Transform playerAssignedLocation;
    public Transform newPlayerLocation;

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            GameObject player = GameManager.instance.FindActivePlayer();
            player.transform.position = playerAssignedLocation.position;

            GameManager.instance.SetPlayerActive(playerIndex); // Set next floor player active
            CameraManager.instance.SetCamGroupActive(camGroupIndex); // Set Camera group to next
            
            GameManager.instance.playerObjs[playerIndex].transform.position = newPlayerLocation.position;
            CameraManager.instance.cameraGroups[CameraManager.instance.currentGroup].SwitchCam(assignedCam);
        }
    }
}
