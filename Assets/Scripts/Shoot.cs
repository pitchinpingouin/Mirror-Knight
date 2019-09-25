using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private EnemyBehaviour behaviour;
    private Transform spawnBulletTransform;
    private float timer;
    [SerializeField] private float fireRate = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<EnemyBehaviour>();
        spawnBulletTransform = transform.Find("spawnTransform");
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (behaviour.ShootingConditionDown)
        {
            ShootFunction();
        }
    }
    // Update is called once per frame
    void ShootFunction()
    {
        if (timer >= fireRate)
        {
            timer = 0.0f;
            BulletFactory.instanceFactory.SpawnBullet(spawnBulletTransform);
        }
    }
}
