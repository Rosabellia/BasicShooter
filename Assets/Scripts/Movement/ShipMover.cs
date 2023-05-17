using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : Mover
{
    private Rigidbody shipRigidBody;

    private void Start()
    {
        shipRigidBody = GetComponent<Rigidbody>();
    }

    public override void Move(float moveSpeed, float direction)
    {
        Vector3 currentPosition = transform.position;
        shipRigidBody.MovePosition(currentPosition = (transform.forward * direction * moveSpeed * Time.deltaTime)); // direction makes it negative or positive
        base.Move(moveSpeed, direction);
    }

    public override void Rotate()
    {
        base.Rotate();
    }
}
