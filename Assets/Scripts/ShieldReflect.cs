using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{
    //Reflect triggers the IsReflected() function of the Object entering in contact with the reflector (this).
    //This class is for the shield only !

    private Move moveScript;

    private float localZ;

    private void Start()
    {
        localZ = transform.localPosition.z;
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.forward * localZ; //Shield is not going to move even if player collides with walls now.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<BulletBehaviour>().IsReflected(this.transform.forward); //Shield reflects bullets in its facing direction.
            moveScript.StopCoroutine("getSlowedThenReturnsMax");
            moveScript.StartCoroutine("getSlowedThenReturnsMax");
        }
    }
}
