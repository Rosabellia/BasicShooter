using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        // throw new System.NotImplementedException();
        Health targetHealth = target.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.ApplyHealthing(healthToAdd);
        }
    }


    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
