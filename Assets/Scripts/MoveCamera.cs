using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject player;
    private Transform cameraTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = player.transform.Find("cameraTarget");
    }

    // Update is called once per frame
    void Update()
    {
        //cameraTarget = player.transform.Find("cameraTarget").position;
        transform.position = cameraTarget.position;
    }
}
