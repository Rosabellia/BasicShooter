using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points = 0;
    public List<PlayerController> players = new List<PlayerController>();
    public List<AIController> enemies = new List<AIController>();

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
