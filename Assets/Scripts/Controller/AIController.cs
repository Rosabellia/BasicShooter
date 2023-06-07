using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Chase, Flee, Patrol, Attack, Scan, BacktoPoint}
    public AIState currentState = AIState.Guard;
    private float lastStateChangeTime = 0f;
    public GameObject target;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        base.Start();
    }

    public override void Update()
    {
        MakeDesisions();
        base.Update();
    }

    public void MakeDesisions()
    {
        switch (currentState)
        {
            case AIState.Guard:
                // Do that states behavior
                DoGuardState();
                // Check for transistions
                break;
            case AIState.Chase:
                // Do that states behavior
                DoChaseState();

                // Check for transistions
                break;
            case AIState.Flee:
                // Do that states behavior
                DoFleeState();
                // Check for transistions
                break;
            case AIState.Patrol:
                // Do that states behavior
                DoPatrolState();
                // Check for transistions
                break;
            case AIState.Attack:
                // Do that states behavior
                DoAttackState();
                // Check for transistions
                break;
            case AIState.Scan:
                // Do that states behavior
                DoScanState();
                // Check for transistions
                break;
            case AIState.BacktoPoint:
                // Do that states behavior
                DoReturnState();
                // Check for transistions
                break;
            default:
                Debug.LogWarning("AI controller doesn't have that state implemented");
                break;
        }
    }

    private void DoReturnState()
    {
        //throw new NotImplementedException();
    }

    private void DoScanState()
    {
        //throw new NotImplementedException();
    }

    private void DoAttackState()
    {
        //throw new NotImplementedException();
    }

    private void DoPatrolState()
    {
        //throw new NotImplementedException();
    }

    private void DoFleeState()
    {
        //throw new NotImplementedException();
    }

    private void DoChaseState()
    {
        //throw new NotImplementedException();
        //Turn to fave target
        pawn.RotateTowards(target.transform.position);
        //Move Forward
        pawn.MoveForward();
    }

    private void DoGuardState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
