using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTargetAccordingToMousePosition : MonoBehaviour
{
    private InputManager inputManager;
    private Transform cameraTargetTransform;
    [SerializeField] private float divisionFactorToPlaceTargetBetweenPlayerAndMouse;
    [SerializeField] private float maxDistanceBetweenPlayerAndMouseToMoveTarget;
    [SerializeField] private float minDistanceBetweenPlayerAndMouseToMoveTarget;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        cameraTargetTransform = transform.Find("cameraTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, inputManager.lookAtTarget) < minDistanceBetweenPlayerAndMouseToMoveTarget)
        {
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x, cameraTargetTransform.localPosition.y, 0);
        }
        else
        if (Vector3.Distance(transform.position, inputManager.lookAtTarget) >= minDistanceBetweenPlayerAndMouseToMoveTarget && Vector3.Distance(transform.position, inputManager.lookAtTarget) <= maxDistanceBetweenPlayerAndMouseToMoveTarget)
        {
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x, cameraTargetTransform.localPosition.y, (Vector3.Distance(transform.position, inputManager.lookAtTarget) - minDistanceBetweenPlayerAndMouseToMoveTarget) / divisionFactorToPlaceTargetBetweenPlayerAndMouse);
        }

        else
        {
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x, cameraTargetTransform.localPosition.y, (maxDistanceBetweenPlayerAndMouseToMoveTarget - minDistanceBetweenPlayerAndMouseToMoveTarget) / divisionFactorToPlaceTargetBetweenPlayerAndMouse);
        }
    }
}
