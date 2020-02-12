using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{
    //Reflect triggers the IsReflected() function of the Object entering in contact with the reflector (this).
    //This class is for the shield only !

    private Move moveScript;
    private LifeManager lifeShieldScript;

    private void Start()
    {
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        lifeShieldScript = GetComponent<LifeShield>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            //Shield reflects bullets in its facing direction.
            other.GetComponent<BulletBehaviour>().IsReflected(this.transform.forward); 
            
            //TODO: use events
            moveScript.StopCoroutine("getSlowedThenReturnsMax");
            moveScript.StartCoroutine("getSlowedThenReturnsMax");

            lifeShieldScript.TakeDamage(other.GetComponent<BulletBehaviour>().damage);
        }
    }
}
