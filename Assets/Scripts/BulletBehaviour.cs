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

    private bool activeCoroutine = false;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        isReflected = false;
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
            if (!activeCoroutine)
            {
                activeCoroutine = !activeCoroutine;
                StartCoroutine("queueBullet");
            }
            
        }
    }

    IEnumerator queueBullet()
    {
        yield return new WaitForSeconds(3.0f);
        BulletFactory.instanceFactory.EnqueueBullet(gameObject);
    }
}


