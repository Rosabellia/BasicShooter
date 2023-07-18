using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class FastShooterPowerup : Powerup
{
    public float shootDelay = 0;
    private float originalDelay;

    public override void Apply(PowerupManager target)
    {
        SpaceShipPawn targetShootSpeed = target.GetComponent<SpaceShipPawn>();

        originalDelay = targetShootSpeed.shotCooldownTime;
        targetShootSpeed.shotCooldownTime = shootDelay;

    }

    public override void Remove(PowerupManager target)
    {
        SpaceShipPawn targetShootSpeed = target.GetComponent<SpaceShipPawn>();
        targetShootSpeed.shotCooldownTime = originalDelay;
    }
}
