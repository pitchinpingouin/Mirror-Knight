using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AbstractBehaviour
{
    public bool isReflected
    {
        get;
        set;
    }
    public Vector3 directionVector;

    private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        isReflected = false;
        player = GameObject.FindGameObjectWithTag("Player");
        directionVector = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReflected)
        {
            lookAtTarget = player.transform.position;
            directionVector = lookAtTarget - this.transform.position;
            horizontalDirection = directionVector.x;
            forwardDirection = directionVector.z;
        }
        else
        {
            lookAtTarget = transform.position + directionVector;
            horizontalDirection = directionVector.x;
            forwardDirection = directionVector.z;

        }
    }
}
