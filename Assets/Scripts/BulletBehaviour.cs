using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AbstractBehaviour
{
    /*
    private Color redColor;
     private Color blueColor;
     private Color blueEmission;
     private Color redEmission;
     */


    private MeshRenderer renderer;
    private Light lightComponent;

    public bool isReflected
    {
        get;
        set;
    }
    public Vector3 directionVector;



    private bool alreadyPassedInLoopOnce = false;

    [SerializeField] private bool willNOTFollowPlayer;

    private GameObject player;

    private void Awake()
    {
        lightComponent = GetComponent<Light>();
        renderer = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        alreadyPassedInLoopOnce = false;
        isReflected = willNOTFollowPlayer;

        if (willNOTFollowPlayer)
        {
            ChangeColorToBlue();
            lookAtTarget = player.transform.position;
            directionVector = lookAtTarget - this.transform.position;
        }
        else
        {
            ChangeColorToRed();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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

            if (!alreadyPassedInLoopOnce)
            {
                alreadyPassedInLoopOnce = !alreadyPassedInLoopOnce;
                ChangeColorToBlue();
                
            }

            if (Vector3.Distance(transform.position, player.transform.position) > 40.0f)
            {
                BulletTooFarFromPlayer();
            }
        }
    }


    public void QueueBullet()
    {
        BulletFactory.instanceFactory.EnqueueBullet(gameObject);
    }

    void ChangeColorToBlue()
    {
        lightComponent.color = Color.blue;
        renderer.material.SetColor("_EmissionColor", Color.blue);
        renderer.material.color = Color.blue;
    }

    void ChangeColorToRed()
    {
        lightComponent.color = Color.red;
        renderer.material.SetColor("_EmissionColor", Color.red);
        renderer.material.color = Color.red;
    }

    private void BulletTooFarFromPlayer()
    {
        QueueBullet();
    }
}


