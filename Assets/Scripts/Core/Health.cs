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
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " did " + damage + " amount");
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
