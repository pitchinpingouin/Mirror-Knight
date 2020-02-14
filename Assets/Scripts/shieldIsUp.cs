using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldIsUp : MonoBehaviour
{
    private GameObject shields;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        shields = transform.Find("shields").gameObject;
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.shieldDown)
        {
            shields.SetActive(true);
        }

        if (inputManager.shieldUp)
        {
            shields.SetActive(false);
        }
    }
}
