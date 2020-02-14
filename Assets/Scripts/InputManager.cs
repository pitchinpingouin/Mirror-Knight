using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractBehaviour
{
    [SerializeField] public Camera mainCamera;
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

    public bool shardDown
    {
        get;
        private set;
    }

    public bool shardUp
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
            pointerPositionInGame = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(mainCamera.transform.position, pointerPositionInGame, Color.blue);
        }

        horizontalDirection = Input.GetAxisRaw("Horizontal");
        forwardDirection = Input.GetAxisRaw("Vertical");

        shardDown = Input.GetButtonDown("shard");
        shardUp = Input.GetButtonUp("shard");

        shieldDown = Input.GetButtonDown("shieldUp");
        shieldUp = Input.GetButtonUp("shieldUp");
        
        escapeButtonDown = Input.GetButtonDown("Exit");


        //A DEPLACER, ne pas laisser ca dans inputManager.
        if (escapeButtonDown)
        {
            Application.Quit();
        }
    }
}
