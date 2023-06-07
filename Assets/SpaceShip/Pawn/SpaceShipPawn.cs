using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipMover))]
public class SpaceShipPawn : Pawn
{
    private const float ForwardDirection = 1f; // Makes ship move forward
    private const float BackwardDirection = -1f; // negatives make the ship move backwards
    public float forwardMoveSpeed = 50f; // How fast the ship moves forward
    public float backwardMoveSpeed = 30f; // How fast the ship moves backward
    public float shipRotationSpeed = 30f; // How fast the ship turns
    public float fireForce = 1000f;
    public float damageDone = 20f;
    public float shellLifespan = 2f;
    public float shotCooldownTime = 1f;
    public GameObject shellPrefab;

    private float secondsSinceLastShot = Mathf.Infinity;



    public override void MoveBackward()
    {
        mover.Move(backwardMoveSpeed, BackwardDirection); // Send the speed and direction to the mover component
        base.MoveBackward();
    }

    public override void MoveForward()
    {
        mover.Move(forwardMoveSpeed, ForwardDirection);
        base.MoveForward();
    }

    public override void Rotate(float direction)
    {
        mover.Rotate(shipRotationSpeed, direction);
        base.Rotate(direction);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<ShipMover>();
        shooter = GetComponent<ShipShooter>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
        base.Update();
    }

    public override void Shoot()
    {
        if (secondsSinceLastShot > shotCooldownTime)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            secondsSinceLastShot = 0f;
            base.Shoot();
        }
        
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //transform.rotation - Quaternion.RotateTowards(transform.rotation, targetRotation, shipRotationSpeed);
    }
}
