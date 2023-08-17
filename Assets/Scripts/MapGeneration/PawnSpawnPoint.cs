using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public Pawn spawnedPawn;
    

    public virtual void Start()
    {
        GameManager.Instance.pawnSpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.pawnSpawnPointsToAdd.Add(this);
        GameManager.Instance.pawnSpawnPoints.Remove(this);
    }
}
