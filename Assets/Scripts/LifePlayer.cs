using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : LifeManager
{
    public GameObject shields;
    [SerializeField] private float damageRadius;
    public Vector3 damagePosition;

    override protected void Start()
    {
        base.Start();
        isGoingToDieNextHit = false;
    }

    public override void TakeDamage(int damageOrHeal)
    {
        if (isGoingToDieNextHit)
        {
                Die();
        }
        else
        {
            if(damageOrHeal > 0)
            {
                //Loose shields
                shields.transform.parent = null;
                //TODO: activate the trigger zone to regrab the shield
                shields.GetComponent<SphereCollider>().enabled = true;

                //Rejects the player and the other bullets with an explosive force.
                GetComponent<Rigidbody>().AddExplosionForce(damageOrHeal, damagePosition, damageRadius);

                //Make the player vulnerable on next hit.
                isGoingToDieNextHit = true;
            }
        }
    }
}
