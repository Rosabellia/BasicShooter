using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points = 0; // Player points
    public List<Controller> players = new List<Controller>(); // List of players
    public List<Controller> enemies = new List<Controller>(); // List of enemies

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
        
    }
}
