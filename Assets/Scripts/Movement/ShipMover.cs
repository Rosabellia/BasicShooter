using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    public override void Rotate(float rotationSpeed, float direction)
    {
        float yAngle = direction * Time.deltaTime * rotationSpeed;
        transform.Rotate(0f, yAngle, 0f);
        base.Rotate(rotationSpeed, direction);
    }
}
