using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeShield : LifeManager
{
    [SerializeField] private float timeToRepair;
    private float timer;
    private Light spotlight;

    MeshRenderer mRenderer;
    BoxCollider bCollider;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spotlight = GetComponentInChildren<Light>();
        mRenderer = GetComponent<MeshRenderer>();
        bCollider = GetComponent<BoxCollider>();
    }

    //TODO: Regenerate shield Health

    IEnumerator WaitBeforeRepair()
    {
        timer = 0.0f;

        while(timer < timeToRepair)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        currentHealth = maxHealth;

        bCollider.enabled = true;
        mRenderer.enabled = true;
        spotlight.gameObject.SetActive(true);
    }

    // Update is called once per frame
    protected override void Die()
    {
        mRenderer.enabled = false;
        bCollider.enabled = false;
        spotlight.gameObject.SetActive(false);
        StartCoroutine("WaitBeforeRepair");
    }
}
