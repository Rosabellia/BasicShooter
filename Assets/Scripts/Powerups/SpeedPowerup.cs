using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SpeedPowerup : Powerup
{
    public float speed = 1000;
    private float originalSpeed;

    public override void Apply(PowerupManager target)
    {
        SpaceShipPawn targetSpeed = target.GetComponent<SpaceShipPawn>();

        originalSpeed = targetSpeed.baseforwardMoveSpeed;

        targetSpeed.baseforwardMoveSpeed = speed;

    }

    public override void Remove(PowerupManager target)
    {
        SpaceShipPawn targetSpeed = target.GetComponent<SpaceShipPawn>();
        targetSpeed.baseforwardMoveSpeed = originalSpeed;
    }
}
