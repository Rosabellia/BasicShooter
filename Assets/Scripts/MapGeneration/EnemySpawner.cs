using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnedPawn;
    public float spawnDelay = 200f;
    public bool spawnImmediately = true;
    private float secondsSinceLastSpawn;


    public void Start()
    {
        if (spawnImmediately)
        {
            Instantiate(spawnedPawn, transform.position, Quaternion.identity);
        }
        else
        {
            secondsSinceLastSpawn = spawnDelay;
        }
    }

    public void Update()
    {
        secondsSinceLastSpawn += Time.deltaTime;
        if (GameManager.Instance.enemies.Count < GameManager.Instance.maxEnemies)
        {
            if (secondsSinceLastSpawn >= spawnDelay)
            {
                Instantiate(spawnedPawn, transform.position, Quaternion.identity);
            }
        }
    }
}
