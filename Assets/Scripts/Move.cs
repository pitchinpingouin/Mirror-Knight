using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private AbstractBehaviour behaviour;
    [SerializeField] private float maxSpeed;
    private float currentSpeed;

    private Vector3 direction;
    // Start is called before the first frame update
    void ComputeDirection()
    {
        direction = Vector3.forward * behaviour.forwardDirection + Vector3.right * behaviour.horizontalDirection;
        direction.Normalize();
    }

    private void Start()
    {
        currentSpeed = maxSpeed;
        behaviour = GetComponent<AbstractBehaviour>();
    }
    // Update is called once per frame
    void Update()
    {
        ComputeDirection();
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
