using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooterPickup : MonoBehaviour
{
    AudioSource pickupNoise;

    public FastShooterPowerup fastShooterPowerup;
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
            manager.Add(fastShooterPowerup);
            Destroy(gameObject);
        }
    }
}
