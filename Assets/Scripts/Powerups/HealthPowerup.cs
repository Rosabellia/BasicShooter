using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float HealthToAdd;
    public override void Apply(PowerupManager target)
    {
        //throw new System.NotImplementedException();
    }

    public override void Remove(PowerupManager target)
    {
        //throw new System.NotImplementedException();
    }
}
