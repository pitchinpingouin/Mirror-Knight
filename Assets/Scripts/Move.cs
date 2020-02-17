using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    protected AbstractBehaviour behaviour;
    [SerializeField] protected float maxSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float slowRecoverFactor;
    protected float currentSpeed;

    //Needed to restore previous state, after a dash or a slow for example.
    protected float previousSpeed;

    protected Vector3 direction;

    protected virtual void ComputeDirection()
    {
        direction = Vector3.forward * behaviour.forwardDirection + Vector3.right * behaviour.horizontalDirection;
        direction.Normalize();
    }

    protected virtual void Start()
    {
        SetCurrentSpeedToMaxSpeed();
        behaviour = GetComponent<AbstractBehaviour>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        ComputeDirection();
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    public void SetSpeedtoValue(float speedValue)
    {
        previousSpeed = currentSpeed;
        currentSpeed = speedValue;
    }

    public void SetCurrentSpeedToSlowSpeed()
    {
        previousSpeed = currentSpeed;
        currentSpeed = slowSpeed;
    }

    public void SetCurrentSpeedToMaxSpeed()
    {
        previousSpeed = currentSpeed;
        currentSpeed = maxSpeed;
    }

    public IEnumerator getSlowedThenReturnsToPrevious()
    {
        //TODO: remember last state, then restore it.
        
        float timer = 0.0f;
        while (timer <= 1.0f)
        {
            currentSpeed = Mathf.SmoothStep(slowSpeed, maxSpeed, timer);
            timer += slowRecoverFactor * Time.deltaTime;
            yield return null;
        }
    }
}
