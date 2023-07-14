using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawnSpawner : PawnSpawnPoint
{
    public override void Start()
    {
        base.Start();

        if (GameManager.Instance.playersSpawned != GameManager.Instance.numberOfPlayers)
        {
            Debug.Log("Player Spawned");
            Instantiate(spawnedPawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("All players have been spawned");
        }
    }

    public override void Update()
    {

    }
}
