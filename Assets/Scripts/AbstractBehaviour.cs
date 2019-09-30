using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBehaviour : MonoBehaviour
{

    //An AbstractBehaviour corresponds to a mobile object. It generates its direction and aim.

    public float horizontalDirection
    {
        get;
        protected set;
    }
    public float forwardDirection
    {
        get;
        protected set;
    }

    public Vector3 lookAtTarget
    {
        get;
        protected set;
    }
}
