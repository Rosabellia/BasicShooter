using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public Pawn spawnedPawn;

    private void Start()
    {
        GameManager.Instance.pawnSpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.pawnSpawnPoints.Remove(this);
    }
}
