using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    //TODO: Move this to the player, and not to each shield.


    InputManager inputManager;
    Move moveScript;
    MeshRenderer mRenderer;
    GrabShield grabShieldScript;
    //The burst gameobject is child of the shield.
    public GameObject burst;

    private bool canBurst;

    [SerializeField] private float burstDistance;
    [SerializeField] private float burstDuration;
    [SerializeField] private float burstCooldown;
    // Start is called before the first frame update
    void Start()
    {
        burst.SetActive(false);
        grabShieldScript = GetComponentInParent<GrabShield>();
        mRenderer = GetComponent<MeshRenderer>();
        inputManager = GetComponentInParent<InputManager>();
        moveScript = GetComponentInParent<Move>();
        canBurst = true;
    }

    IEnumerator BurstCoroutine()
    {
        canBurst = false;
        
        //We position the burst in between the shield and the max distance.
        burst.SetActive(false);
        burst.transform.position = transform.position + transform.forward * burstDistance / 2;
        burst.transform.rotation = transform.rotation;
        burst.transform.localScale = new Vector3(
            burst.transform.localScale.x,
            burst.transform.localScale.y,
            burstDistance / 2);

        //Activate the burst and slow player to show impact, if he's still holding the shield.
        if (grabShieldScript.alreadyHasShield) {
            moveScript.StartCoroutine("getSlowedThenReturnsToPrevious");
        }
        burst.SetActive(true);

        //The burst deactivates after its duration.
        yield return new WaitForSeconds(burstDuration);
        burst.SetActive(false);

        //Waiting for cooldown to be able to burst again.
        yield return new WaitForSeconds(burstCooldown - burstDuration);

        canBurst = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.burstDown)
        {
            if (canBurst)
            {
                if (mRenderer.enabled)
                {
                    StartCoroutine("BurstCoroutine");
                }
            }
        }
    }
}
