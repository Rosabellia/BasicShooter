using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BacktoPoint}
    public AIState currentState = AIState.Chase;
    private float lastStateChangeTime = 0f;
    public float AttackRange = 100f;
    public Controller target;
    public Transform post;
    public float felildOfView = 30f;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        post = transform;
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
            case AIState.Idle:
                // Do that states behavior
                DoIdleState();
                // Check for transistions
                foreach (Controller playerController in GameManager.Instance.players)
                {
                    if (CanSee(playerController.gameObject))
                    {
                        target = playerController;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.gameObject))
                    {
                        ChangeAIState(AIState.Scan);
                        return;
                    }
                }
                break;
            case AIState.Chase:
                // Do that states behavior
                DoChaseState();

                // Check for transistions
                if (!CanSee(target.gameObject))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                }
                if(Vector3.SqrMagnitude(target.transform.position - transform.position) <= AttackRange)
                {
                    ChangeAIState(AIState.Attack);
                    return;
                }
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
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) < AttackRange)
                {
                    ChangeAIState(AIState.Chase);
                }
                if (!CanSee(target.gameObject))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }
                break;
            case AIState.Scan:
                // Do that states behavior
                DoScanState();
                // Check for transistions
                foreach (Controller playerController in GameManager.Instance.players)
                {
                    if (CanSee(playerController.gameObject))
                    {
                        target = playerController;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.gameObject))
                    {
                        ChangeAIState(AIState.Idle);
                        return;
                    }
                }
                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.BacktoPoint);
                    return;
                }
                break;
            case AIState.BacktoPoint:
                // Do that states behavior
                DoReturnState();
                // Check for transistions
                if (Vector3.SqrMagnitude(post.transform.position - transform.position) <= 1f)
                {
                    ChangeAIState(AIState.Idle);
                    return;
                }
                break;
            default:
                Debug.LogWarning("AI controller doesn't have that state implemented");
                break;
        }
    }

    private bool CanHear(GameObject targetGameObject)
    {

        return false;
    }

    private bool CanSee(GameObject targetGameObject)
    {
        Vector3 agentToTargetVector = targetGameObject.transform.position - transform.position;
        if (Vector3.Angle(agentToTargetVector ,transform.forward) <= felildOfView)
        {
            Vector3 raycastDirection = targetGameObject.transform.position - pawn.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, raycastDirection.normalized, out hit))
            {
                if(hit.collider != null)
                {
                    return (hit.collider.gameObject == targetGameObject);
                }
            }
        }
        return false;
    }

    private void DoReturnState()
    {
        pawn.RotateTowards(post.transform.position);

        pawn.MoveForward();
    }

    private void DoScanState()
    {
        // Rotate Clockwise
        pawn.Rotate(1);
    }

    private void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);
        pawn.Shoot();
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

    private void DoIdleState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
