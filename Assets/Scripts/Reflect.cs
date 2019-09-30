using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    //Reflect triggers the IsReflected() function of the Object entering in contact with the reflector (this).

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<BulletBehaviour>().IsReflected(this.transform.forward);
        }
    }
}
