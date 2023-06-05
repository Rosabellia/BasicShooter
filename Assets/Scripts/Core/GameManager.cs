using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points = 0; // Player points
    public List<PlayerController> players = new List<PlayerController>(); // List of players
    public List<AIController> enemies = new List<AIController>(); // List of enemies

    private void Awake()
    {
        // Make sure there is only 1 game mannager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attempted to create a second instance of the Game Manager");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject); // Keep the GameManager when level loads in
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
