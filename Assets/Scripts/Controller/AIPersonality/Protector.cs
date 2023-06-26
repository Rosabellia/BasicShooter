using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector : AIController
{
    private Health firstAlly;
    private Health secondAlly;

    public override void Start()
    {

        secondAlly = this.gameObject.GetComponent<Health>();
        base.Start();
    }


    protected void FindAlly()
    {
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
    }
}
