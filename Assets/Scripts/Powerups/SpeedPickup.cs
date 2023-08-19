using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    AudioSource pickupNoise;

    public SpeedPowerup speedPowerup;

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
            manager.Add(speedPowerup);
            Destroy(gameObject);
        }
    }
}
