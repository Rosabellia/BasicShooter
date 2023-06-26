using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseClosestPlayer : AIController
{
    private float firstVector;
    private float secondVector = -1;
    public float huntPlayer = 25f;

    private void FindClosestPlayer()
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
}
