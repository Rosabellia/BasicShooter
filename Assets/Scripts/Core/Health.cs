using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth = 100f;

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
        //When taking damage the current health is subtracked by the damage
        // minHealth and MaxHealth keep the player within their health limitations
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " did " + damage + " amount");
        // When the currentHealth is at Min Health or less, player dies
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(source);
        }
    }

    private void Die(Pawn source)
    {
        //throw new NotImplementedException();
        Destroy(gameObject);
    }
}
