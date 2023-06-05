using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceShipPawn))]
public class PlayerController : Controller
{
    // Let the Desinger change the key buttons
    public KeyCode forwardKeyCode;
    public KeyCode backwardKeyCode;
    public KeyCode rightKeyCode;
    public KeyCode leftKeyCode;

    // Make the component a variable
    private SpaceShipPawn playerpawn;

    // Start is called before the first frame update
    public override void Start()
    {
        // Make sure we have the SpaceShipPawn
        playerpawn = GetComponent<SpaceShipPawn>();
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Add(this);
        }

        base.Start();
    }

    private void OnDestroy()
    {
        // When the Player gets destroyed, they get removed from the Player list
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Remove(this);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs(); // Tell the component that the player has pressed a button
        base.Update();
    }

    private void ProcessInputs()
    {
        // Assign each buttion
        if (Input.GetKey(forwardKeyCode)) // Move forward
        {
            playerpawn.MoveForward();
        }
        if (Input.GetKey(backwardKeyCode)) // Move Backward
        {
            playerpawn.MoveBackward();
        }
        if (Input.GetKey(rightKeyCode)) // Turn to the right
        {
            playerpawn.Rotate(1f); 
        }
        if (Input.GetKey(leftKeyCode)) // Turn to the left
        {
            playerpawn.Rotate(-1f); 
        }
        
    }
}
