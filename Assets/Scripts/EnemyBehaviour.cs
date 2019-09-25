using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : AbstractBehaviour
{
    private float fireConditionTimer = 3.0f;
    public bool ShootingConditionDown
    {
        get;
        private set;
    }
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ShootingConditionDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > fireConditionTimer)
        {
            timer = 0.0f;
            ShootingConditionDown = true;
            
        }
        lookAtTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
        horizontalDirection = Mathf.Cos(Time.time);
        forwardDirection = Mathf.Sin(Time.time);
    }
    
}
