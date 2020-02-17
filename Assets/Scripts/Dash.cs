using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Move
{
    //Dash only works for Player at the moment. If we want enemies to dash, we have to refactor the code.
    //Dash implements Move.
    InputManager inputManager;

    private bool isDashing;
    private bool canDash;
    private float timer;

    TrailRenderer trailDash;

    //dashTimer < dashCooldown.
    [SerializeField] private float dashTimer;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;

    protected override void Start()
    {
        //trailDash = GetComponent<TrailRenderer>();
        inputManager = GetComponent<InputManager>();
        base.Start();
        canDash = true;
        isDashing = false;
    }
    // Update is called once per frame
    protected override void ComputeDirection()
    {
        if (!isDashing)
        {
            base.ComputeDirection();
        }
        else
        {
            //Keep the direction like it is.
        }
    }

    IEnumerator DashCoroutine()
    {
        timer = 0.0f;
        canDash = false;

        direction = inputManager.pointerPositionInGame - transform.position;
        direction.Normalize();

        isDashing = true;
        SetSpeedtoValue(dashSpeed);

        while(timer < dashTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SetSpeedtoValue(previousSpeed);
        isDashing = false;

        while(timer < dashCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        canDash = true;
    }

    protected override void Update()
    {
        if (inputManager.dashDown)
        {
            if (canDash)
            {
                StartCoroutine("DashCoroutine");
            }
        }

        base.Update();
    }
}
