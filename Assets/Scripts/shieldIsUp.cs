using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldIsUp : MonoBehaviour
{
    [SerializeField] private float slowSpeed;

    private Move moveScript;
    private GameObject shield;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("shield").gameObject;
        moveScript = GetComponent<Move>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.leftClickDown)
        {
            moveScript.changeSpeedtoValue(slowSpeed);
            shield.SetActive(true);
        }

        if (inputManager.leftClickUp)
        {
            moveScript.SetCurrentSpeedToMaxSpeed();
            shield.SetActive(false);
        }
    }
}
