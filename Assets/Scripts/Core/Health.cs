using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;
[System.Serializable]

public class HealthChanged : UnityEvent<float,float>
{
    
}

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth = 100f;
    public HealthChanged OnHealthChanged = new HealthChanged();
    public PlayerHUD playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Pawn source)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        OnHealthChanged.Invoke(currentHealth, maxHealth);
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(source);
        }
    }

    public void ApplyHealthing(float value)
    {

        Debug.Log(value);
        if (value < 0)
        {
            Debug.LogWarning("Attempted to heal for negative amount.");
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth + value, minHealth, maxHealth);
        OnHealthChanged.Invoke(currentHealth, maxHealth);
    }

    private void Die(Pawn source)
    {
        // Get the player indext if killed to a player
        int playerIndext = GameManager.Instance.GetPlayerIndext(source);


        // Award points to that player
        if (playerIndext != -1)
        {
            GameManager.Instance.points[playerIndext] += source.pointsOnKilled;
        }

        int myIndext = GameManager.Instance.GetPlayerIndext(gameObject.GetComponent<Pawn>());

        if (myIndext >= 0 && playerIndext == -1)
        {
            GameManager.Instance.lives[myIndext] -= 1;

        }
        else
        {
            GameManager.Instance.SpawnEnemy();
        }

        Destroy(gameObject);
    }
}
