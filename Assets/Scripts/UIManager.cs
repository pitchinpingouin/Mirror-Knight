using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager InstanceUI;
    // Start is called before the first frame update
    void Awake()
    {
        InstanceUI = this;
    }

}
