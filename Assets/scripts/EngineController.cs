﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : MonoBehaviour
{

    public ParticleSystem frontRightThrustEngine;
    public ParticleSystem frontLeftThrustEngine;
    public ParticleSystem rearRightThrustEngine;
    public ParticleSystem rearLeftThrustEngine;
    public ParticleSystem[] EngineSystems;
    public bool isActive;

    private void Update()
    {
        isActive = frontRightThrustEngine.isPlaying || frontLeftThrustEngine.isPlaying || rearRightThrustEngine.isPlaying || rearLeftThrustEngine.isPlaying;
    }
    public void AvoidEffect(bool Right)
    {
        frontRightThrustEngine.startLifetime = 0.5f;
        frontLeftThrustEngine.startLifetime = 0.5f;
        rearRightThrustEngine.startLifetime = 0.5f;
        rearLeftThrustEngine.startLifetime = 0.5f;
        if (Right)
        {
            frontLeftThrustEngine.Play();
            rearLeftThrustEngine.Play();
        }
        else
        {
            frontRightThrustEngine.Play();
            rearRightThrustEngine.Play();
        }
    }
    public void ThrustEngineEffect(Vector3 targetRotation, Vector3 playerRotation)
    {
        float rotationDifference = targetRotation.y - playerRotation.y;
        frontRightThrustEngine.startLifetime = frontLeftThrustEngine.startLifetime = rearRightThrustEngine.startLifetime = rearLeftThrustEngine.startLifetime = 0.15f;

        if (rotationDifference > 1)
        {
            frontLeftThrustEngine.Play();
            rearRightThrustEngine.Play();
        }
        else if (rotationDifference < -1)
        {
            frontRightThrustEngine.Play();
            rearLeftThrustEngine.Play();
        }
        
    }
    public void MainEngineEffect(float enginePower)
    {
        foreach (ParticleSystem engSys in EngineSystems)
        {
            engSys.startLifetime = enginePower;
        }
    }
}
