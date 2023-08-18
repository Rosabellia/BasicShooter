using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

//[RequireComponent(typeof(SpaceShipPawn))]
public class PlayerController : Controller
{
    // Let the Desinger change the key buttons
    public KeyCode forwardKeyCode;
    public KeyCode backwardKeyCode;
    public KeyCode rightKeyCode;
    public KeyCode leftKeyCode;
    public KeyCode shootKeyCode;
    public KeyCode pauseKeycode;

    // Start is called before the first frame update
    public override void Start()
    {

        
        // Make sure we have the SpaceShipPawn
        pawn = GetComponent<Pawn>();
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Add(this);
            AdjustPlayerCameras();
            SetMovementControls();

            Debug.Log(GameManager.Instance.players.Count); // on respan gives 2
        }

        

        base.Start();
    }

    private void OnDestroy()
    {
        
        // When the Player gets destroyed, they get removed from the Player list
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Remove(this);

            Debug.Log(GameManager.Instance.players.Count); // on respawn gives 1
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
        // Assign each buttion
        if (!GameManager.Instance.IsPaused) {

            if (Input.GetKey(forwardKeyCode))
            {
                pawn.MoveForward();
            }
            if (Input.GetKey(backwardKeyCode))
            {
                pawn.MoveBackward();
            }
            if (Input.GetKey(rightKeyCode))
            {
                pawn.Rotate(1f);
            }
            if (Input.GetKey(leftKeyCode))
            {
                pawn.Rotate(-1f);
            }
            if (Input.GetKeyDown(shootKeyCode))
            {
                pawn.Shoot();
            }

        }
        if (Input.GetKeyDown(pauseKeycode))
        {
            GameManager.Instance.TogglePause();
        }
    }
    private void AdjustPlayerCameras()
    {
        // Get player 1's camera
        Camera player1Camera = GameManager.Instance.players[0].GetComponentInChildren<Camera>();

        if (GameManager.Instance.players.Count == 1)
        {
            // Get player 1's camera

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 0.5f, 1f);

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 1f, 1f);
        }
        else
        {
            // Get player 2's camera
            Camera player2Camera = GameManager.Instance.players[1].GetComponentInChildren<Camera>();

            // Set player 1's camera posistion
            player1Camera.rect = new Rect(0, 0, 0.5f, 1f);
            Debug.Log(player1Camera.rect);

            // Set player 2's camera posistion
            player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1f);
        }
    }
    private void SetMovementControls()
    {
        PlayerController player1Controller = GameManager.Instance.players[0].GetComponentInChildren<PlayerController>();

        if (GameManager.Instance.players.Count == 1)
        {
            forwardKeyCode = KeyCode.W;
            backwardKeyCode = KeyCode.S;
            rightKeyCode = KeyCode.D;
            leftKeyCode = KeyCode.A;
            shootKeyCode = KeyCode.Space;
        }
        else
        {
            // Get player 2's controlls
            PlayerController player2Controller = GameManager.Instance.players[1].GetComponentInChildren<PlayerController>();

            forwardKeyCode = KeyCode.UpArrow;
            backwardKeyCode = KeyCode.DownArrow;
            rightKeyCode = KeyCode.RightArrow;
            leftKeyCode = KeyCode.LeftArrow;
            shootKeyCode = KeyCode.Mouse0;
        }
    }
}
