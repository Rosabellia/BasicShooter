using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points = 0; // Player points
    public int numberOfPlayers = 1;
    [HideInInspector] public int playersSpawned = 0;
    [HideInInspector] public int maxEnemies = 10;
    public List<Controller> players = new List<Controller>(); // List of players
    public List<Controller> enemies = new List<Controller>(); // List of enemies
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();
    public enum Difficulty { Easy, Medium, Hard }

    public Difficulty difficulty = Difficulty.Easy;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attempted to create a second instance of the Game Manager");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (difficulty == Difficulty.Easy)
        {

        }
        else if (difficulty == Difficulty.Medium)
        {

        }
        else if(difficulty == Difficulty.Hard) 
        {

        }
        else
        {
            Debug.LogWarning("No difficult was set");
        }
    }

    void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while(spawn.spawnedPawn != null) 
        {
            spawn = GetRandomSpawnPoint();
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }


    }

    private PawnSpawnPoint GetRandomSpawnPoint()
    {
        return pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Count)];
    }
}
