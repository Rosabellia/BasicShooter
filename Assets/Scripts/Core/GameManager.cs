using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameStateChangeEvent : UnityEvent<GameState, GameState>
{

}
public enum GameState { TitleState, OptionsState, GameplayerState, GameOverState, CreditsState, PauseState };
public class GameManager : MonoBehaviour
{
    public GameStateChangeEvent OnGameStateChanged = new GameStateChangeEvent();
    public static GameManager Instance;
    public int numberOfPlayers = 1;
    public List<int> points = new List<int>();
    public List<int> lives = new List<int>();
    [HideInInspector] public int playersSpawned = 0;
    public int maxEnemiesCount = 10;


    public List<Controller> players = new List<Controller>(); // List of players
    public List<Controller> enemies = new List<Controller>(); // List of enemies

    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();
    public List<PawnSpawnPoint> pawnSpawnPointsToAdd = new List<PawnSpawnPoint>();
    
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<Waypoint> waypoints = new List<Waypoint>();
    public List<Waypoint> waypointsToAdd = new List<Waypoint>();

    public GameObject playerPrefab;
    

    public IEnumerator SpawnShipsNextFrame()
    {
        yield return null;
        // this code runs on the next frame
        SpawnPlayers();
        if (playersSpawned == numberOfPlayers)
        {
            SpawnEnemies();
        }
    }
    public IEnumerator SpawnPlayerShipNextFrame()
    {
        SpawnPlayer();
        yield return null;
    }

    public bool multiplayer = true;

    public bool IsPaused
    {
        get
        {
            return (currentGameState == GameState.PauseState);
        }
    }

    public bool PlayersHaveLives
    {
        get
        {
            int totalLives = 0;
            foreach(int playerLives in lives)
            {
                totalLives += playerLives;
            }
            return (totalLives > 0);
        }
    }


    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty difficulty = Difficulty.Easy;

    public GameState currentGameState = GameState.TitleState;
    private GameState previousGameState;


    public void ChangeGameState(GameState state)
    {
        previousGameState = currentGameState;
        currentGameState = state;
        OnGameStateChanged.Invoke(previousGameState, currentGameState);
    }

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

    public void setSingleplayer()
    {
        // Set singleplayer for menu UI
        numberOfPlayers = 1;
        multiplayer = false;

        points.Add(0);
        lives.Add(3);

        StartGame();
    }

    public void setMultiplayer()
    {
        // Set multiplayer for menu UI
        numberOfPlayers = 2;
        multiplayer = true;

        points.Add(0);
        points.Add(0);
        lives.Add(3);
        lives.Add(3);

        StartGame();
    }

    public void SpawnEnemy()
    {
        if (enemies.Count == maxEnemiesCount)
        {
            Debug.Log("All enemies has been spawned");
            return;
        }

        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        if (spawn.spawnedPawn == null)
        {
            spawn = GetRandomSpawnPoint();

            int enemyNumer = UnityEngine.Random.Range(0, enemyPrefabs.Count);

            GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyNumer], spawn.transform.position, Quaternion.identity);
            spawn.spawnedPawn = spawnedEnemy.GetComponent<Pawn>();
            enemies.Add(spawnedEnemy.GetComponent<Controller>());
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAK
            
        }
        else
        {
            SpawnEnemies();
        }
    }
    public void SpawnPlayer()
    {
        
        if (pawnSpawnPoints.Count < numberOfPlayers)
        {
            Debug.LogError("Not enough spawn points");
            return;
        }
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        if (spawn.spawnedPawn == null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
            spawn.spawnedPawn = spawnedPlayer.GetComponent<Pawn>();
            playersSpawned += 1;
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS

        }
        else
        {
            SpawnPlayer();
        }

    }

    public void SpawnPlayers()
    {
        while (playersSpawned < numberOfPlayers) 
        {
            SpawnPlayer();
        }

    }

    public void SpawnEnemies()
    {
        while (enemies.Count < maxEnemiesCount) 
        {
            SpawnEnemy();
        }

        RestartSpawnPoints();
    }


    private PawnSpawnPoint GetRandomSpawnPoint()
    {
        if (pawnSpawnPoints.Count == 0)
        {
            foreach (PawnSpawnPoint pawnSpawnPoint in pawnSpawnPoints)
            {
                pawnSpawnPoints.Add(pawnSpawnPoint);
                pawnSpawnPointsToAdd.Remove(pawnSpawnPoint);
            }
        }

        return pawnSpawnPoints[UnityEngine.Random.Range(0, pawnSpawnPoints.Count)];
    }

    public Waypoint GetRandomWaypoint()
    {
        return waypoints[UnityEngine.Random.Range(0, waypoints.Count)];
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        ChangeGameState(GameState.GameplayerState);
        Time.timeScale = 1f;
        StartCoroutine(SpawnShipsNextFrame());
    }

    public void OpenOptionsMenu()
    {
        ChangeGameState(GameState.OptionsState);
    }

    public void ChangeToPreviousGameState()
    {
        ChangeGameState(previousGameState);
    }

    public void ChangeStateToTitle()
    {
        ChangeGameState(GameState.TitleState);
    }

    public void PauseGame()
    {
        ChangeGameState(GameState.PauseState);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        ChangeGameState(GameState.GameplayerState);
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (currentGameState == GameState.PauseState)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public int GetPlayerIndext(Pawn source)
    {
        // Get the controller


        foreach (Controller controller in players)
        {
            if (controller.ControlledPawn == source)
            {
                return (players.IndexOf(controller));
            }
        }
        // Compare the controller with the playercontrollers, return the indext if there is a match

        // If no match, return -1
        return -1;
    }

    public void Respawn()
    {
        // Respawn player

        // Respawn enemy
    }

    public void RestartWaypointCount()
    {
        foreach (Waypoint waypoint in GameManager.Instance.waypointsToAdd)
        {
            waypoints.Add(waypoint);
            waypointsToAdd.Remove(waypoint);
        }
    }
    public void RestartSpawnPoints()
    {
        foreach (PawnSpawnPoint spawnPoint in pawnSpawnPointsToAdd)
        {
            pawnSpawnPoints.Add(spawnPoint);
            pawnSpawnPointsToAdd.Remove(spawnPoint);

        }
    }
}
