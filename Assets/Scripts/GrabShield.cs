using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabShield : MonoBehaviour
{
    private InputManager inputManager;
    public bool canGrabShield;
    public bool alreadyHasShield;
    public GameObject shields;
    public Transform spawnTransform;

    private void Start()
    {
        canGrabShield = false;
        alreadyHasShield = true;
        shields.GetComponent<SphereCollider>().enabled = false;
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        
        if (inputManager.shieldDown)
        {
            if (canGrabShield && !alreadyHasShield)
            {
                shields.GetComponent<SphereCollider>().enabled = false;
                shields.transform.parent = gameObject.transform;
                shields.transform.position = spawnTransform.position;
                shields.transform.rotation = spawnTransform.rotation;

                canGrabShield = false;
                alreadyHasShield = true;
            }
            else 
            if(alreadyHasShield)
            {

                alreadyHasShield = false;
                
                shields.transform.parent = null;

                shields.GetComponent<SphereCollider>().enabled = true;
                canGrabShield = true;

            }
        }
    }
}
