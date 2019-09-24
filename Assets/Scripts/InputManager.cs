using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractBehaviour
{
    [SerializeField] Camera mainCamera;
    private Ray cameraRay;

    public bool leftClickDown
    {
        get;
        private set;
    }

    public bool leftClickUp
    {
        get;
        private set;
    }

    public Vector3 mousePositionOnScreen
    {
        get;
        private set;
    }


    // Start is called before the first frame update
    void Start()
    {
       
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
            lookAtTarget = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(mainCamera.transform.position, lookAtTarget, Color.blue);
        }

        horizontalDirection = Input.GetAxisRaw("Horizontal");
        forwardDirection = Input.GetAxisRaw("Vertical");

        leftClickDown = Input.GetButtonDown("shieldUp");
        leftClickUp = Input.GetButtonUp("shieldUp");
    }
}
