using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    bool isHealing;
    GameObject player;
    LifeManager lifeScript;
    [SerializeField] private int healPerFrame;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeScript = player.GetComponent<LifeManager>();
        isHealing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHealing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHealing = false;
        }
    }

    private void Update()
    {
        if (isHealing)
        {
            lifeScript.TakeDamage(-healPerFrame);
        }
    }
}
