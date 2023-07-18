using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooterPickup : MonoBehaviour
{
    public FastShooterPowerup fastShooterPowerup;
    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.gameObject.GetComponent<PowerupManager>();
        if (manager)
        {
            manager.Add(fastShooterPowerup);
            Destroy(gameObject);
        }
    }
}
