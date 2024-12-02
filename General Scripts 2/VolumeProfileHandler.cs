using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeProfileHandler : MonoBehaviour
{
    public static VolumeProfileHandler instance;

    public Volume volume;

    public VolumeProfile profileNormal;
    public VolumeProfile profileNightVision;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.Analysis)
            volume.profile = profileNightVision;
        else
            volume.profile = profileNormal;
    }
}
