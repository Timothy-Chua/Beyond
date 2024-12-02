using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextFloor : MonoBehaviour
{
    public FloorTriggerType type;
    public Transform playerAssignedLocation;

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            GameObject player = GameManager.instance.FindActivePlayer();
            player.transform.position = playerAssignedLocation.position;

            if (type == FloorTriggerType.Next)
            {
                GameManager.instance.NextPlayer(); // Set next floor player active
                CameraManager.instance.NextCameraGroup(); // Set Camera group to next
            }
            else if (type == FloorTriggerType.Previous)
            {
                GameManager.instance.PrevPlayer();
                CameraManager.instance.PrevCameraGroup();
            }
        }
    }
}

public enum FloorTriggerType
{
    Next, Previous
}
