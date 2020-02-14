using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTargetAccordingToMousePosition : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTargetTransform;

    private int screenWidth;
    private int screenHeight;

    private Vector2 centerOfScreen;

    private Ray cameraRaySmall;
    private Ray cameraRayBig;

    private Vector3 pointSmallEllipseInGame;
    private Vector3 pointBigEllipseInGame;

    //Floats between 0 and 1 to draw the ellipses with a border. They are percentages of screen width and height.


    //! WARNING: need borderWidthMax > borderWidthMin and borderHeightMax > borderHeightMin !
    //If = 1, it's the screen border. if 0, it's the center. It can be higher than 1.
    [SerializeField] private float borderWidthMaxPercent;
    [SerializeField] private float borderWidthMinPercent;

    [SerializeField] private float borderHeightMaxPercent;
    [SerializeField] private float borderHeightMinPercent;

    //Parameters of the ellipses. "a" represent the large semi-diameter(width), and "b" the little semi-diameter(height). 
    //Min is the small ellipse, Max the big one.

    // a and b are given in number of pixels.
    private int aMin;
    private int bMin;
    private int aMax;
    private int bMax;

    //Coordonates of a point of an ellipse: If the angle of the aimed point is alpha, then
    //we have (xEllipse, yEllipse) = (a * cos(alpha), a * sin(alpha) * b/a) = (a * cos(alpha), b * sin(alpha))

    private float angleInDegrees;
    private Vector2 pointSmallEllipseOnScreen;
    private Vector2 pointBigEllipseOnScreen;

    [SerializeField] private float factorMoveAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        cameraTargetTransform = transform.Find("cameraTarget");

        //The bottom-left of the screen or window is at (0, 0). The top-right of the screen or window is at (Screen.width, Screen.height).

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        centerOfScreen = new Vector2(screenWidth / 2, screenHeight / 2);

        aMin = (int)(screenWidth / 2 * borderWidthMinPercent);
        aMax = (int)(screenWidth / 2 * borderWidthMaxPercent);

        bMin = (int)(screenHeight / 2 * borderHeightMinPercent);
        bMax = (int)(screenHeight / 2 * borderHeightMaxPercent);
    }

    // Update is called once per frame
    void Update()
    {
        //We want to calculate positions based on the angle made by our mousePosition.

        //Vector3.Angle is locked between 0 and 180 degrees. So if the mouse is above the player, OK.
        if(inputManager.pointerPositionInGame.z > transform.position.z)
        {
            angleInDegrees = Vector3.Angle(Vector3.right, (inputManager.pointerPositionInGame - transform.position));
        }
        //If the mouse is below the player, we reverse the sign.
        else
        {
            angleInDegrees = - Vector3.Angle(Vector3.right, (inputManager.pointerPositionInGame - transform.position));
        }

        //Coordinates of the points of our ellipses on screen, in pixels.
        pointSmallEllipseOnScreen = new Vector2(aMin * Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), bMin * Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)) 
            + centerOfScreen;
        pointBigEllipseOnScreen = new Vector2(aMax * Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), bMax * Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)) 
            + centerOfScreen;

        //We now find the coordinates in the game world, by tracing rays from the camera.
        cameraRaySmall = inputManager.mainCamera.ScreenPointToRay(pointSmallEllipseOnScreen);
        
        float rayLengthSmall;
        
        Plane planeIntersectingWithRay = new Plane(Vector3.up, transform.position);

        if (planeIntersectingWithRay.Raycast(cameraRaySmall, out rayLengthSmall))
        {
            pointSmallEllipseInGame = cameraRaySmall.GetPoint(rayLengthSmall);
            Debug.DrawLine(inputManager.mainCamera.transform.position, pointSmallEllipseInGame, Color.green);
        }

        cameraRayBig = inputManager.mainCamera.ScreenPointToRay(pointBigEllipseOnScreen);
        float rayLengthBig;

        if (planeIntersectingWithRay.Raycast(cameraRayBig, out rayLengthBig))
        {
            pointBigEllipseInGame = cameraRayBig.GetPoint(rayLengthBig);
            Debug.DrawLine(inputManager.mainCamera.transform.position, pointBigEllipseInGame, Color.red);
        }





        ///See documentation on Google: cartesian equation of an ellipse.
        // If the equation is > 1, the mouse position is outside of the ellipse. If = 1, the point is part of the ellipse. If < 1, it's inside.

        //If we are inside the small ellipse, then cameraTarget is centered to the player.
        if (((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMin)) * ((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMin))
            + ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMin)) * ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMin))
            < 1)
        {
            //cameraTarget is a child of player, so we just put it's z-localPosition to zero.
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x, cameraTargetTransform.localPosition.y, 0);
        }

        //In that case, the mouse is outside of the small ellipse.
        else
        {
            //If we are between the small and the big ellipses, then we move cameraTarget to a center amount, according to the mousePosition.
            if (((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMax)) * ((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMax))
            + ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMax)) * ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMax))
            < 1)
            {
                cameraTargetTransform.localPosition = new Vector3(
                    cameraTargetTransform.localPosition.x,
                    cameraTargetTransform.localPosition.y,
                    (Vector3.Distance(
                        new Vector3(inputManager.mousePositionOnScreen.x / screenWidth, inputManager.mousePositionOnScreen.y / screenHeight, 0),
                        new Vector3(pointSmallEllipseOnScreen.x / screenWidth, pointSmallEllipseOnScreen.y / screenHeight, 0)
                        )
                    ) * factorMoveAmplitude
                );
            }
            else
            //Here, the mouse is outside the biggest ellipse. The cameraTarget z-localPosition is maxed.

            if (((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMax)) * ((inputManager.mousePositionOnScreen.x - centerOfScreen.x) / (aMax))
            + ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMax)) * ((inputManager.mousePositionOnScreen.y - centerOfScreen.y) / (bMax))
            >= 1)
            {
                cameraTargetTransform.localPosition = new Vector3(
                    cameraTargetTransform.localPosition.x,
                    cameraTargetTransform.localPosition.y,
                    (Vector3.Distance(
                        new Vector3(pointBigEllipseOnScreen.x / screenWidth, pointBigEllipseOnScreen.y / screenHeight, 0), 
                        new Vector3(pointSmallEllipseOnScreen.x /screenWidth, pointSmallEllipseOnScreen.y / screenHeight, 0)
                        )
                    ) * factorMoveAmplitude
                );
            }
        }
    }
}
