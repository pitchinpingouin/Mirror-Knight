using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : AbstractBehaviour
{
    private float fireConditionTimer = 3.0f;
    private Transform targetTransform;
    private float fireDuration = 2.5f;
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
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > fireConditionTimer)
        {
            ShootingConditionDown = true;
            if (timer > fireDuration + fireConditionTimer)
            {
                timer = 0.0f;
                ShootingConditionDown = false;
            }
        }

        pointerPositionInGame = targetTransform.position;
        horizontalDirection = Mathf.Cos(Time.time);
        forwardDirection = Mathf.Sin(Time.time);
    }
    
}
