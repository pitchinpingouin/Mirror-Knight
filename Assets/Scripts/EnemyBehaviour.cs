using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : AbstractBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookAtTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
        horizontalDirection = Mathf.Cos(Time.time);
        forwardDirection = Mathf.Sin(Time.time);
    }
    
}
