using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractBehaviour
{
    public Camera mainCamera;
    private Ray cameraRay;

    public bool escapeButtonDown
    {
        get;
        private set;
    }

    public bool shieldDown
    {
        get;
        private set;
    }

    public bool shieldUp
    {
        get;
        private set;
    }

    public bool dashDown
    {
        get;
        private set;
    }

    public bool dashUp
    {
        get;
        private set;
    }

    public bool burstDown
    {
        get;
        private set;
    }

    public bool burstUp
    {
        get;
        private set;
    }

    public bool lightDown
    {
        get;
        private set;
    }

    public bool lightUp
    {
        get;
        private set;
    }

    public Vector3 mousePositionOnScreen
    {
        get;
        private set;
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionOnScreen = Input.mousePosition;
        cameraRay = mainCamera.ScreenPointToRay(mousePositionOnScreen);

        float rayLength;
        Plane planeIntersectingWithRay = new Plane(Vector3.up, transform.position);
        if(planeIntersectingWithRay.Raycast(cameraRay, out rayLength))
        {
            pointerPositionInGame = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(mainCamera.transform.position, pointerPositionInGame, Color.blue);
        }

        horizontalDirection = Input.GetAxisRaw("Horizontal");
        forwardDirection = Input.GetAxisRaw("Vertical");

        lightDown = Input.GetButtonDown("ThrowLight");
        lightUp = Input.GetButtonUp("ThrowLight");

        dashUp = Input.GetButtonUp("Dash");
        dashDown = Input.GetButtonDown("Dash");

        burstUp = Input.GetButtonUp("Burst");
        burstDown = Input.GetButtonDown("Burst");

        shieldDown = Input.GetButtonDown("GrabShield");
        shieldUp = Input.GetButtonUp("GrabShield");
        
        escapeButtonDown = Input.GetButtonDown("Exit");


        //A DEPLACER, ne pas laisser ca dans inputManager.
        if (escapeButtonDown)
        {
            Application.Quit();
        }
    }
}
