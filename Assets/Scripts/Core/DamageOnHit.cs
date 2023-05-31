using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damage = 20f;
    public Pawn owner;

    private void OnTriggerEnter(Collider other)
    {
        // Get the health component
        Health otherHealth = other.gameObject.GetComponent<Health>();

        //Make sure there is a health component
        if (otherHealth != null)
        {
            //Do Damage
            otherHealth.TakeDamage(damage, owner);
        }

        //Asuming it's a bullet or something
        Destroy(gameObject);
    }

}
