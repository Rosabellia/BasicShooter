using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceShipPawn))]
public class PlayerController : Controller
{
    public KeyCode forwardKeyCode;
    public KeyCode backwardKeyCode;
    public KeyCode rightKeyCode;
    public KeyCode leftKeyCode;

    private SpaceShipPawn playerpawn;

    // Start is called before the first frame update
    public override void Start()
    {
        playerpawn = GetComponent<SpaceShipPawn>();
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Add(this);
        }

        base.Start();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Remove(this);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
        base.Update();
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(forwardKeyCode))
        {
            playerpawn.MoveForward();
        }
        if (Input.GetKey(backwardKeyCode))
        {
            playerpawn.MoveBackward();
        }
        if (Input.GetKey(rightKeyCode))
        {
            playerpawn.Rotate(1f); // turns to the right
        }
        if (Input.GetKey(leftKeyCode))
        {
            playerpawn.Rotate(-1f); // turns to the left
        }
        
    }
}
