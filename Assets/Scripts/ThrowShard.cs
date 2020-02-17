using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowShard : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private float force; //TODO: watch the talk about how to throw a grenade.
    [SerializeField] private GameObject shard;
    [SerializeField] private Transform spawnTransform;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.lightDown)
        {
            shard.SetActive(false);
            shard.transform.position = spawnTransform.position;

            direction = (inputManager.pointerPositionInGame - spawnTransform.position);
            direction.Normalize();

            shard.SetActive(true);
            shard.GetComponent<Rigidbody>().AddForce(force * direction);
        }
    }
}
