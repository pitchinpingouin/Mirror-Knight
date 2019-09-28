using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithBullets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<BulletBehaviour>().QueueBullet();
        }
    }
}
