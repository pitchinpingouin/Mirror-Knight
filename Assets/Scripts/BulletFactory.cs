using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private GameObject bullet1;
    private Queue<GameObject> queueBullets;
    [SerializeField] private int nbBulletsInQueue = 15;

    public static BulletFactory instanceFactory;

    void Start()
    {
        instanceFactory = this;
        queueBullets = new Queue<GameObject>();
        for(int i = 0; i < nbBulletsInQueue; i++)
        {
            GameObject bulletInstantiated = Instantiate(bullet1, null);
            bulletInstantiated.SetActive(false);
            queueBullets.Enqueue(bulletInstantiated);
        }
    }

    public GameObject SpawnBullet(Transform spawnTransform)
    {
        GameObject bulletToSpawn = null;

        if (queueBullets.Count > 0)
        {
            bulletToSpawn = queueBullets.Dequeue();
            bulletToSpawn.transform.position = spawnTransform.position;
            bulletToSpawn.SetActive(true);
        }
        else
        {
            bulletToSpawn = Instantiate(bullet1, null);
            bulletToSpawn.transform.position = spawnTransform.position;
        }

        return bulletToSpawn;
    }

    public void EnqueueBullet(GameObject bulletToEnqueue)
    {
        bulletToEnqueue.SetActive(false);
        queueBullets.Enqueue(bulletToEnqueue);
    }
}
