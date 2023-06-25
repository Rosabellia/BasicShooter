using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BacktoPoint, FindPlayer, FindLowestAllie}
    public enum AIPersonality { Protector, TargetLowHealthPlayer, TargetFarthestPlayer, TargetClosestPlayer, FromSeen };

    public AIState currentState = AIState.Scan;
    public AIPersonality personality= AIPersonality.FromSeen;

    private float lastStateChangeTime = 0f;
    public float attackRange = 1500f;
    public GameObject target;
    public Transform post;
    public float fieldOfView = 30f;

    private float firstVector;
    private float secondVector = -1;
    public float huntPlayer = 25f;

    private Health firstAlly;
    private Health secondAlly;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        if (GameManager.Instance)
        {
            GameManager.Instance.enemies.Add(this);
        }
        post = transform;
        secondAlly = this.gameObject.GetComponent<Health>();
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
                        target = playerController.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.gameObject))
                    {
                        ChangeAIState(AIState.Scan);
                        return;
                    }
                }

                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.Scan);
                    return;
                }

                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();
                // Check for transitions
                if (!CanSee(target))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }

                if (Vector3.SqrMagnitude(target.transform.position - transform.position) <= attackRange)
                {
                    Debug.Log("Work!!!!");
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
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) > attackRange)
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
                    // If player is within feild of view
                    if (CanSee(playerController.gameObject))
                    {
                        target = playerController.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }

                    // If player is making noise near by
                    if (CanHear(playerController.gameObject))
                    {
                        ChangeAIState(AIState.Scan);
                        return;
                    }
                }

                // If it has been 3 seconds
                if (Time.time - lastStateChangeTime > 3f)
                {

                    if (personality == AIPersonality.Protector)
                    {
                        ChangeAIState(AIState.FindLowestAllie);
                    }

                     else if (personality == AIPersonality.TargetFarthestPlayer)
                    {
                        ChangeAIState(AIState.FindPlayer);
                    }

                    else if (personality == AIPersonality.TargetClosestPlayer)
                    {
                        ChangeAIState(AIState.FindPlayer);
                    }

                    else
                    {
                        ChangeAIState(AIState.BacktoPoint);
                    }

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
            case AIState.FindLowestAllie:
                // Do that states behavior
                 
                 if (!target)
                {
                    // Check each aiController for distance
                    foreach (Controller aiController in GameManager.Instance.enemies)
                    {
                        // Get first aiController
                        firstAlly = aiController.gameObject.GetComponent<Health>();

                        if (firstAlly != this.gameObject.GetComponent<Health>())
                        {
                            // change secondAlly to firstAlly
                            if (firstAlly.currentHealth < secondAlly.currentHealth)
                            {
                                secondAlly = firstAlly;

                            }

                            // if firstAlly is the same as SecondVector make that the target
                            if (firstAlly.gameObject == secondAlly.gameObject)
                            {
                                target = aiController.gameObject;
                            }
                        }
                    }
                }

                // Check for transistions
                if (Time.time - lastStateChangeTime > huntPlayer)
                {
                    ChangeAIState(AIState.BacktoPoint);
                    return;
                }

                DoChaseState();
                break;
            case AIState.FindPlayer:
                // Do that states behavior
                // Do that states behavior

                if (personality == AIPersonality.TargetClosestPlayer)
                {
                    //If there is no target
                    if (!target)
                    {
                        // Check each playerController for distance
                        foreach (Controller playerController in GameManager.Instance.players)
                        {
                            // Get first playerController
                            firstVector = Vector3.Distance(playerController.transform.position, transform.position);

                            // If seconfVector is at default
                            if (secondVector == -1)
                            {
                                secondVector = firstVector;

                            }

                            // change secondVector to firstVector
                            if (firstVector < secondVector)
                            {
                                secondVector = firstVector;

                            }

                            // if firstVector is the same as SecondVector make that the target
                            if (firstVector == secondVector)
                            {
                                target = playerController.gameObject;
                            }

                        }
                    }
                }

                if (personality == AIPersonality.TargetFarthestPlayer)
                {
                    //If there is no target
                    if (!target)
                    {
                        // Check each playerController for distance
                        foreach (Controller playerController in GameManager.Instance.players)
                        {
                            // Get first playerController
                            firstVector = Vector3.Distance(playerController.transform.position, transform.position);

                            // If seconfVector is at default
                            if (secondVector == -1)
                            {
                                secondVector = firstVector;

                            }

                            // change secondVector to firstVector
                            if (firstVector > secondVector)
                            {
                                secondVector = firstVector;

                            }

                            // if firstVector is the same as SecondVector make that the target
                            if (firstVector == secondVector)
                            {
                                target = playerController.gameObject;
                            }

                        }
                    }
                }

                // Check for transistions

                // Change state back to scan
                if (Time.time - lastStateChangeTime > huntPlayer)
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                }

                // Move towards target
                DoChaseState();

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
        if (Vector3.Angle(agentToTargetVector, transform.forward) <= fieldOfView)
        {
            Vector3 raycastDirection = targetGameObject.transform.position - pawn.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, raycastDirection.normalized, out hit))
            {
                if (hit.collider != null)
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
        if (target)
        {
            pawn.RotateTowards(target.transform.position);
            //Move Forward
            pawn.MoveForward();
        }
        if (!target)
        {
            Debug.LogWarning("Target is not assigned");
        }
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
