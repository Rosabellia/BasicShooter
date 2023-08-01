using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    public int numberOfPlayers = 2;
    public List<int> points = new List<int>();
    public List<int> lives = new List<int>();
    [HideInInspector] public int playersSpawned = 0;
    [HideInInspector] public int maxEnemies = 10;
    public List<Controller> players = new List<Controller>(); // List of players
    public List<Controller> enemies = new List<Controller>(); // List of enemies
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();
    public GameObject playerPawn;

    public bool multiplayer = false;

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
        AdjustPlayerCameras();
    }

    void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while(spawn.spawnedPawn != null) 
        {
            spawn = GetRandomSpawnPoint();
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }

        AdjustPlayerCameras();

        Instantiate(playerPawn, spawn.transform.position, Quaternion.identity);

    }

    private void AdjustPlayerCameras()
    {
        // Get player 1's camera
        Camera player1Camera = players[0].GetComponentInChildren<Camera>();

        if (numberOfPlayers == 1)
        {
            // Get player 1's camera

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 0.5f, 1f);

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 1f, 1f);
            Debug.Log(player1Camera.rect);
        }
        else
        {
            // Get player 2's camera
            Camera player2Camera = players[1].GetComponentInChildren<Camera>();

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 0.5f, 1f);
            Debug.Log(player1Camera.rect);

            // Set player 2's camera posistion
            player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1f);
            Debug.Log(player1Camera.rect);
        }
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
}
