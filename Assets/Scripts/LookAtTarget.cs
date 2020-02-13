using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private AbstractBehaviour behaviour;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<AbstractBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(behaviour.pointerPositionInGame);
    }
}
