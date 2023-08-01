using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected Mover mover;
    protected Shooter shooter;

    public int pointsOnKilled = 100;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void MoveForward()
    {

    }
    public virtual void MoveBackward()
    {

    }
    public virtual void Rotate(float direction)
    {

    }

    public virtual void Shoot()
    {

    }

    public virtual void RotateTowards(Vector3 targetPosition)
    {

    }
}
