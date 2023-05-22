using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipMover))]
public class SpaceShipPawn : Pawn
{
    private const float moveForward = 1;
    private const float movebackward = -1;
    public float forwardMoveSpeed = 50f;
    public float backwardMoveSpeed = 30f;
    public float shipRotationSpeed = 30f;

    public override void MoveBackward()
    {
        Debug.Log("Move Backwards");
        base.MoveBackward();
    }

    public override void MoveForward()
    {
        Debug.Log("Move Forwards");
        mover.Move(forwardMoveSpeed, 1);
        base.MoveForward();
    }

    public override void Rotate(float direction)
    {
        Debug.Log("Rotate");
        mover.Rotate(shipRotationSpeed, direction);
        base.Rotate(direction);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        //mover = GetComponent(ShipMover);
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
