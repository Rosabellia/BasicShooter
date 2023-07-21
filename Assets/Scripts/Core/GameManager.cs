using System.Collections;
using System.Collections.Generic;
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
    public List<int> points = new List<int>();
    [HideInInspector] public int playersSpawned = 0;
    [HideInInspector] public int maxEnemies = 10;
    public List<Controller> players = new List<Controller>(); // List of players
    public List<Controller> enemies = new List<Controller>(); // List of enemies
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();
    public GameObject playerPawn;

    public bool IsPaused
    {
        get
        {
            return (currentGameState == GameState.PauseState);
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

    void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while(spawn.spawnedPawn != null) 
        {
            spawn = GetRandomSpawnPoint();
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }

        Instantiate(playerPawn, spawn.transform.position, Quaternion.identity);

    }

    private PawnSpawnPoint GetRandomSpawnPoint()
    {
        return pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Count)];
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        ChangeGameState(GameState.GameplayerState);
        Time.timeScale = 1f;
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
}
