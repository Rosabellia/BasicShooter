using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup 
{
    public bool isPermenat = false;
    public float duration;
    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);
}
