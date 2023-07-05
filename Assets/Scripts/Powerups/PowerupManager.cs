using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups = new List<Powerup>();
    public List<Powerup> powerupsToRemove = new List<Powerup>();
    // Start is called before the first frame update
    private void Start()
    {
        DecementPowerupTimers();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    // The Add function will eventually add a powerup
    public void Add (Powerup PowerupToAdd)
    {
        PowerupToAdd.Apply(this);
        if (!PowerupToAdd.isPermenat)
        {
            powerups.Add(PowerupToAdd);
        }
    }

    // The Add function will eventually add a powerup
    public void Remove (Powerup PowerupToRemove)
    {
        PowerupToRemove.Remove(this);
        powerups.Remove(PowerupToRemove);
    }

    public void DecementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            powerup.duration -= Time.deltaTime;

            if (powerup.duration <= 0)
            {
                powerupsToRemove.Add(powerup);
            }
        }
    }

    private void RemovePowerUpQue()
    {
        foreach(Powerup powerup in powerupsToRemove)
        {
            powerups.Remove(powerup);
        }
        powerupsToRemove.Clear();
    }
}