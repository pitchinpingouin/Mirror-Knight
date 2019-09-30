using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AbstractBehaviour
{
    private MeshRenderer renderer;
    private Light lightComponent;
    [SerializeField] private int damage;

    public bool isReflected
    {
        get;
        set;
    }
    public Vector3 directionVector;

    private GameObject psGameObject;

    private ParticleSystem ps;

    [SerializeField] private bool willNOTFollowPlayer;

    private GameObject player;

    private void Awake()
    {
        psGameObject = transform.Find("psGameObject").gameObject;
        ps = psGameObject.GetComponentInChildren<ParticleSystem>();
        lightComponent = GetComponent<Light>();
        renderer = GetComponent<MeshRenderer>();
        //ps = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Start is called before the first frame update
    void OnEnable()
    {
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
            if (Vector3.Distance(transform.position, player.transform.position) > 30.0f)
            {
                BulletTooFarFromPlayer();
            }
        }
    }

    public void IsReflected(Vector3 newDirectionVector)
    {
        isReflected = true;
        ChangeColorToBlue();
        StartCoroutine("PewPewParticles");
        directionVector = newDirectionVector;
        //lookAtTarget = transform.position + directionVector;
        horizontalDirection = directionVector.x;
        forwardDirection = directionVector.z;
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

    IEnumerator PewPewParticles()
    {
        psGameObject.transform.parent = null;
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        psGameObject.transform.parent = this.transform;
        psGameObject.transform.position = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isReflected)
        {
            other.GetComponent<LifeManager>().TakeDamage(damage);
            QueueBullet();
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<LifeManager>().TakeDamage(damage);
            QueueBullet();
        }

        
    }
}


