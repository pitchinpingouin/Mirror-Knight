using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private AbstractBehaviour behaviour;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float slowSpeed;
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
        SetCurrentSpeedToMaxSpeed();
        behaviour = GetComponent<AbstractBehaviour>();
    }
    // Update is called once per frame
    void Update()
    {
        ComputeDirection();
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    public void changeSpeedtoValue(float speedValue)
    {
        currentSpeed = speedValue;
    }

    public void SetCurrentSpeedToMaxSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public IEnumerator getSlowedThenReturnsMax()
    {
        float timer = 0.0f;
        while (timer <= 1.0f)
        {
            currentSpeed = Mathf.SmoothStep(slowSpeed, maxSpeed, timer);
            timer += 2 * Time.deltaTime;
            yield return null;
        }
    }
}
