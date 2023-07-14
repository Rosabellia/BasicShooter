using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : PawnSpawnPoint
{
    public float spawnDelay = 200f;
    public bool spawnImmediately = true;
    private float secondsSinceLastSpawn;


    public override void Start()
    {
        if (spawnImmediately)
        {
            Instantiate(spawnedPawn, transform.position, Quaternion.identity);
        }
        else
        {
            secondsSinceLastSpawn = spawnDelay;
        }
        base.Start();
    }

    public override void Update()
    {
        secondsSinceLastSpawn += Time.deltaTime;
        if (GameManager.Instance.enemies.Count < GameManager.Instance.maxEnemies)
        {
            if (secondsSinceLastSpawn >= spawnDelay)
            {
                Instantiate(spawnedPawn, transform.position, Quaternion.identity);
            }
        }

        base.Update();
    }
}
