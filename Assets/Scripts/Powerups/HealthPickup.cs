using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup healthPowerup;

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.transform.parent.GetComponent<PowerupManager>();
        if (manager)
        {
            manager.Add(healthPowerup);
            Destroy(gameObject);
        }
    }
}
