using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipMover : Mover
{
    private Rigidbody shipRigidBody;

    private void Start()
    {
        //Make sure the pawn has a Rigibody
        shipRigidBody = GetComponent<Rigidbody>();
    }

    public override void Move(float moveSpeed, float direction)
    {
        Vector3 currentPosition = transform.position; // Get the current position in XYZ vectors
        // Add to current location the, forward movement multiplied by speed, direction, and delta time (time not set by frame rate)
        shipRigidBody.MovePosition(currentPosition + (transform.forward * moveSpeed * direction  * Time.deltaTime));
        base.Move(moveSpeed, direction);
    }

    public override void Rotate(float rotationSpeed, float direction)
    {
        // the ship's current direction multiplied by telta time and rotation speed
        float yAngle = direction * Time.deltaTime * rotationSpeed;
        transform.Rotate(0f, yAngle, 0f); // Change the Y axis without messing with X and Z
        base.Rotate(rotationSpeed, direction);
    }
}
