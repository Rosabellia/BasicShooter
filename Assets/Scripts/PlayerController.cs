using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceShipPawn))]
public class PlayerController : Controller
{
    private SpaceShipPawn playerpawn;

    // Start is called before the first frame update
    public override void Start()
    {
        playerpawn = GetComponent<SpaceShipPawn>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
        base.Update();
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerpawn.MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerpawn.MoveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerpawn.Rotate(-1f); // turns to the left
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerpawn.Rotate(1f); // turns to the right
        }
    }
}
