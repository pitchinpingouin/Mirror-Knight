using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AbstractBehaviour
{
    private MeshRenderer mRenderer;
    private Light lightComponent;
    [SerializeField] public int damage;

    public bool isReflected
    {
        get;
        set;
    }
    public Vector3 directionVector;

    public Color RedEmission;
    public Color BlueEmission;


    private GameObject psGameObject;

    private ParticleSystem ps;

    [SerializeField] private bool willNOTFollowPlayer;

    private GameObject player;

    private void Awake()
    {
        psGameObject = transform.Find("psGameObject").gameObject;
        ps = psGameObject.GetComponentInChildren<ParticleSystem>();
        lightComponent = GetComponent<Light>();
        mRenderer = GetComponent<MeshRenderer>();
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
            pointerPositionInGame = player.transform.position;
            directionVector = pointerPositionInGame - this.transform.position;
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
            pointerPositionInGame = player.transform.position;
            directionVector = pointerPositionInGame - this.transform.position;
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
        pointerPositionInGame = transform.position + 1000 * directionVector;
        horizontalDirection = directionVector.x;
        forwardDirection = directionVector.z;
    }
    

    public void QueueBullet()
    {
        BulletFactory.instanceFactory.EnqueueBullet(gameObject);
    }

    void ChangeColorToBlue()
    {
        mRenderer.material.SetColor("_EmissionColor", BlueEmission);
        mRenderer.material.SetColor("_Color", Color.blue);
    }

    void ChangeColorToRed()
    {
        mRenderer.material.SetColor("_EmissionColor", RedEmission);
        mRenderer.material.SetColor("_Color", Color.red);
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
            other.GetComponent<LifePlayer>().damagePosition = transform.position;
            other.GetComponent<LifeManager>().TakeDamage(damage);
            QueueBullet();
        }

        
    }
}


