using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class HealthChanged : UnityEvent<float,float>
{

}

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth = 100f;
    public HealthChanged OnHealthChanged = new HealthChanged();

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
        OnHealthChanged.Invoke(currentHealth, maxHealth);
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " did " + damage + " amount");
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
    }

    private void Die(Pawn source)
    {
        // Get the player indext if killed to a player
        int playerIndext = GameManager.Instance.GetPlayerIndext(source);

        // Award points to theat player
        if (playerIndext != -1)
        {
            GameManager.Instance.points[playerIndext] += source.pointsOnKilled;
        }

        Destroy(gameObject);
    }
}
