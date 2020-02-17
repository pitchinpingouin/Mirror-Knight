using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetectsPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<GrabShield>().canGrabShield = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<GrabShield>().canGrabShield = false;
        }
    }
}
