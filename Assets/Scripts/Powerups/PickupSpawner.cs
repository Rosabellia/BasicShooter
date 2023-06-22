using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    private GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;

    private void Start()
    {
        spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    private void Update()
    {
        if (spawnedPickup == null)
        {
            if (Time.time > nextSpawnTime)
            {
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
