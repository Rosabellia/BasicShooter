using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public GameObject spawnedPawn;
    

    public virtual void Start()
    {
        GameManager.Instance.pawnSpawnPoints.Add(this);
    }

    public virtual void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.pawnSpawnPoints.Remove(this);
    }
}
