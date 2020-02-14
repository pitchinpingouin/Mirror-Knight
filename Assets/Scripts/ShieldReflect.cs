using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{
    //Reflect triggers the IsReflected() function of the Object entering in contact with the reflector (this).
    //This class is for the shield only !

    private Move moveScript;
    private LifeManager lifeShieldScript;
    [SerializeField] private float limitAngleToReflect;

    private void Start()
    {
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        lifeShieldScript = GetComponent<LifeShield>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            //If the projectile doesn't come from behind, reflect it.
            if(Vector3.Angle(other.transform.position - transform.position, transform.forward) < limitAngleToReflect)
            {
                //Shield reflects bullets in its facing direction.
                other.GetComponent<BulletBehaviour>().IsReflected(transform.forward);
            }
            else
            {
                other.GetComponent<BulletBehaviour>().QueueBullet();
            }
            
            //TODO: use events. If the player holds the shields, then slow him.
            if(transform.parent.parent != null)
            {
                moveScript.StopCoroutine("getSlowedThenReturnsMax");
                moveScript.StartCoroutine("getSlowedThenReturnsMax");
            }
            
            //The shield takes damage.
            lifeShieldScript.TakeDamage(other.GetComponent<BulletBehaviour>().damage);
        }
    }
}
