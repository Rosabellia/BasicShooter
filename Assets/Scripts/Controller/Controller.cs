using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Pawn pawn;
    protected Health health;
    public Pawn ControlledPawn
    {
        get 
        { 
            return pawn; 
        }
    }
    

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }
}
