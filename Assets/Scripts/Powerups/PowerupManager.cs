using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups = new List<Powerup>();
    public List<Powerup> powerupsToRemove = new List<Powerup>();
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    // The Add function will eventually add a powerup
    public void Add(Powerup powerupToAdd)
    {
        powerupToAdd.Apply(this);
        if (!powerupToAdd.isPermanent)
        {
            powerups.Add(powerupToAdd);
        }
    }

    // The Remove function may eventually remove a powerup
    public void Remove(Powerup powerupToRemove)
    {
        powerupToRemove.Remove(this);
        powerups.Remove(powerupToRemove);
    }

    public void DecrementPowerupTimers()
    {
        // One-at-a-time, put each object in "powerups" into the variable "powerup" and do the loop body on it
        foreach (Powerup powerup in powerups)
        {
            // Subtract the time it took to draw the frame from the duration
            powerup.duration -= Time.deltaTime;
            // If time is up, we want to remove this powerup
            if (powerup.duration <= 0)
            {
                powerupsToRemove.Add(powerup);
            }
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        // Now that we are sure we are not iterating through "powerups", remove the powerups that are in our temporary list
        foreach (Powerup powerup in powerupsToRemove)
        {
            Remove(powerup);
            powerups.Remove(powerup);
        }
        // And reset our temporary list
        powerupsToRemove.Clear();
    }
}