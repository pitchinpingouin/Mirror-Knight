using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<BulletBehaviour>().isReflected = true;
            other.GetComponent<BulletBehaviour>().directionVector = this.transform.forward;
        }
    }
}
