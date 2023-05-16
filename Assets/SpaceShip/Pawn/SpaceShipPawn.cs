using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPawn : Pawn
{
    public override void MoveBackward()
    {
        Debug.Log("Move Backwards");
        base.MoveBackward();
    }

    public override void MoveForward()
    {
        Debug.Log("Move Forwards");
        base.MoveForward();
    }

    public override void Rotate(float direction)
    {
        Debug.Log("Rotate");
        base.Rotate(direction);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
