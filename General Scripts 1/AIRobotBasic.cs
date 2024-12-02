using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AIRobotBasic : AIEnemy
{
    [Header("Light")]
    protected Light robotLight;
    public Color colorLightPatrol;
    public Color colorLightInvestigate;
    public Color colorLightChase;

    protected override void Awake()
    {
        robotLight = GetComponentInChildren<Light>();

        base.Awake();
    }

    protected override void Start()
    {
        walkSFX = SoundManager.instance.robotWalkSFX;
        runSFX = SoundManager.instance.robotRunSFX;

        base.Start();
    }

    protected override void LookingPlayer(Vector3 player)
    {
        //robotLight.color = colorInvestigate;
        robotLight.color = Color.Lerp(robotLight.color, colorLightInvestigate, Time.deltaTime);

        base.LookingPlayer(player);
    }

    protected override void Chase()
    {
        //robotLight.color = colorChase;
        robotLight.color = Color.Lerp(robotLight.color, colorLightChase, Time.deltaTime);

        base.Chase();
    }

    protected override void StartPatrol()
    {
        //robotLight.color = colorPatrol;
        robotLight.color = Color.Lerp(robotLight.color, colorLightPatrol, Time.deltaTime);

        base.StartPatrol();
    }

    // Add disable coroutine
}
