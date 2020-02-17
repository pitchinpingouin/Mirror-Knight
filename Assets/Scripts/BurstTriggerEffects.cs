using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTriggerEffects : MonoBehaviour
{
    private List<Collider> colliders;


    //Don't forget to initialize it !
    private void Awake()
    {
        colliders = new List<Collider>();
    }

    //TODO: Layer 9 is for bullets. Need to trigger on enemies too. Layer 12 is for enemies.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            colliders.Add(other);
            other.GetComponent<Move>().SetCurrentSpeedToSlowSpeed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12)
        {
            colliders.Remove(other);
            other.GetComponent<Move>().SetCurrentSpeedToMaxSpeed();
        }
    }

    private void OnDisable()
    {
        if(colliders.Count > 0)
        {
            for(int i = 0; i < colliders.Count; i++)
            {
                colliders[i].GetComponent<Move>().SetCurrentSpeedToMaxSpeed();
                
            }

            colliders.Clear();
        }
    }
}
