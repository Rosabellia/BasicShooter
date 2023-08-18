using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BacktoPoint, FindPlayer, FindLowestAllie}
    public enum AIPersonality { Protector, TargetFarthestPlayer, TargetClosestPlayer, FromSeen };

    public Waypoint currentWaypoint;

    public AIState currentState = AIState.Scan;
    public AIPersonality personality= AIPersonality.FromSeen;

    [HideInInspector] public float lastStateChangeTime = 0f;
    public float attackRange = 1500f;
    public GameObject target;
    public Transform post;
    public float fieldOfView = 30f;
    public float fleeDistance = 50;
    public float hearingDistance = 100f;


    private float firstVector;
    private float secondVector = -1;
    public float huntPlayer = 25f;

    private Health firstAlly;
    private Health secondAlly;
    private Health thisEnemy;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        if (GameManager.Instance)
        {
            GameManager.Instance.enemies.Add(this);
        }
        post = transform;
        secondAlly = this.gameObject.GetComponent<Health>();
        thisEnemy = this.gameObject.GetComponent<Health>();
        currentWaypoint = GameManager.Instance.GetRandomWaypoint();
        base.Start();
    }

    private void OnDestroy()
    {
        GameManager.Instance.enemies.Remove(this);
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
                CanSensePlayer();

                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.Scan);
                    return;
                }

                break;
            case AIState.Chase:
                if (target != null)
                {
                    // Do that state's behavior
                    DoChaseState(target.transform.position);
                    // Check for transitions
                    if (!CanSee(target))
                    {
                        target = null;
                        ChangeAIState(AIState.Scan);
                        return;
                    }

                    if (Vector3.SqrMagnitude(target.transform.position - transform.position) <= attackRange)
                    {
                        ChangeAIState(AIState.Attack);
                        return;
                    }
                }
                break;
            case AIState.Flee:
                // Do that states behavior
                DoFleeState();
                // Check for transistions
                if (Time.time - lastStateChangeTime > fleeDistance)
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                }

                break;
            case AIState.Patrol:
                // Do that states behavior
                DoPatrolState();
                CanSensePlayer();

                // Check for transistions
                break;
            case AIState.Attack:
                // Do that states behavior
                DoAttackState();
                // Check for transistions
                if (target != null)
                {
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
                    if (thisEnemy.currentHealth < 50f)
                    {
                        ChangeAIState(AIState.Flee);
                    }
                }
                break;
            case AIState.Scan:
                // Do that states behavior
                DoScanState();
                // Check for transistions
                
                foreach (Controller playerController in GameManager.Instance.players)
                {
                    if (playerController != null)
                    {
                        // If player is within feild of view
                        if (CanSee(playerController.gameObject))
                        {
                            target = playerController.gameObject;
                            ChangeAIState(AIState.Chase);
                            return;
                        }
                    }
                    
                }

                // If it has been 3 seconds
                if (Time.time - lastStateChangeTime > 3f)
                {
                    if (personality == AIPersonality.Protector)
                    {
                        ChangeAIState(AIState.FindLowestAllie);
                    }
                    if (personality == AIPersonality.TargetFarthestPlayer)
                    {
                        ChangeAIState(AIState.FindPlayer);
                    }
                    if (personality == AIPersonality.TargetClosestPlayer)
                    {
                        ChangeAIState(AIState.FindPlayer);
                    }
                    if (personality == AIPersonality.FromSeen)
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
                    ChangeAIState(AIState.Patrol);
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
                        if (aiController != null)
                        {
                            // Get first aiController
                            firstAlly = aiController.gameObject.GetComponent<Health>();
                            if (firstAlly != null && secondAlly != null)
                            {
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

                    }
                }

                // Check for transistions
                if (Time.time - lastStateChangeTime > huntPlayer)
                {
                    ChangeAIState(AIState.BacktoPoint);
                    return;
                }
                if (target != null)
                {
                    DoChaseState(target.transform.position);
                }

                break;
            case AIState.FindPlayer:
                // Do that states behavior

                CanSensePlayer();

                if (personality == AIPersonality.TargetClosestPlayer)
                {
                    //If there is no target
                    if (!target)
                    {
                        // Check each playerController for distance
                        foreach (Controller playerController in GameManager.Instance.players)
                        {
                            if (playerController != null)
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
                }

                if (personality == AIPersonality.TargetFarthestPlayer)
                {
                    //If there is no target
                    if (!target)
                    {
                        // Check each playerController for distance
                        foreach (Controller playerController in GameManager.Instance.players)
                        {
                            if (playerController != null)
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
                }

                // Check for transistions

                // Change state back to scan
                if (Time.time - lastStateChangeTime > huntPlayer)
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                }

                if (target)
                {
                    // Move towards target
                    DoChaseState(target.transform.position);
                }

                break;
            default:
                Debug.LogWarning("AI controller doesn't have that state implemented");
                break;
        }
    }

    protected bool CanHear(GameObject targetGameObject)
    {
        if (Vector3.Distance(transform.position, targetGameObject.transform.position) <= hearingDistance)
        {
            // ... then we can hear the target
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool CanSee(GameObject targetGameObject)
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

    protected void DoReturnState()
    {
        pawn.RotateTowards(post.transform.position);

        pawn.MoveForward();
    }

    protected void DoScanState()
    {
        // Rotate Clockwise
        pawn.Rotate(1);
    }

    protected void DoAttackState()
    {
        if (target != null)
        {
            pawn.RotateTowards(target.transform.position);
            pawn.Shoot();
        }
    }

    protected void DoPatrolState()
    {
        
        if (GameManager.Instance.waypoints.Count >= 0)
        {

            DoChaseState(currentWaypoint.gameObject.transform.position);

            if (Vector3.Distance(pawn.transform.position, currentWaypoint.gameObject.transform.position) <= 1)
            {

                currentWaypoint = GameManager.Instance.GetRandomWaypoint(); ;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        GameManager.Instance.RestartWaypointCount();
    }

    protected void DoFleeState()
    {
        if (target)
        {
            Vector3 vectorTarget = target.transform.position - pawn.transform.position;

            Vector3 vectorAwayFromTarget = -vectorTarget;

            float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
            float percentOffDistance = targetDistance / fleeDistance;

            percentOffDistance = Mathf.Clamp01(percentOffDistance);

            float flippedPercentOffDistance = 1 - percentOffDistance;


            Vector3 fleeVector = vectorAwayFromTarget.normalized * flippedPercentOffDistance;

            DoChaseState(fleeVector);


        }
    }

    protected void DoChaseState(Vector3 location)
    {
        //throw new NotImplementedException();
        //Turn to fave target
        pawn.RotateTowards(location);
        //Move Forward
        pawn.MoveForward();
    }

    protected void DoIdleState()
    {
        //throw new NotImplementedException();
    }

    protected void CanSensePlayer()
    {
        foreach (Controller playerController in GameManager.Instance.players)
        {
            if (playerController != null)
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
                    target = playerController.gameObject;
                    ChangeAIState(AIState.Scan);
                    return;
                }
            }
            
        }
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
