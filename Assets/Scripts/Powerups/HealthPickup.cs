using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup healthPowerup;
    AudioSource pickupNoise;

    private void Start()
    {
        pickupNoise = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.gameObject.GetComponent<PowerupManager>();
        if (manager)
        {
            pickupNoise.Play(0);
            manager.Add(healthPowerup);
            Destroy(gameObject);
        }
    }
}
