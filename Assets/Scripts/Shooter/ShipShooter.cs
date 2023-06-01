using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipShooter : Shooter
{
    public Transform firepointTransform;

    public override void Shoot(GameObject shellPrefab, float fireforce, float damageDone, float lifespan)
    {
        //Spawn in bullet
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
        //Set value for bullet
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();
        if (damageOnHit)
        {
            damageOnHit.damage = damageDone;
            damageOnHit.owner = GetComponent<Pawn>();
        }
        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.AddForce(firepointTransform.forward * fireforce);
        }

        Destroy(newShell, lifespan);

    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}
