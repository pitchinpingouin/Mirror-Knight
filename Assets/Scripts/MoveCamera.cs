using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject player;
    private Transform cameraTargetTransform;
    private Vector3 newPosition;
    [SerializeField] float timeDurationTravelling;
    private float timer = 0.0f;

    private Vector3 currentVelocity;

    void Start()
    {
        currentVelocity = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTargetTransform = player.transform.Find("cameraTarget");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraTargetTransform.position, ref currentVelocity, timeDurationTravelling);
    }
}
