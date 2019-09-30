using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : LifeManager
{
    private float slowDamageOverTime = 1.0f;

    [SerializeField] private float maxRange;
    [SerializeField] private float maxIntensity;


    private Light flameLight;
    private GameObject torchGameObject;

    // Update is called once per frame
    override protected void Start()
    {
        base.Start();
        torchGameObject = transform.Find("torch").gameObject;
        flameLight = torchGameObject.GetComponentInChildren<Light>();
        StartCoroutine("SlowlyKillTheFlame");
    }

    private void Update()
    {
        float percentage = (float)currentHealth / (float)maxHealth;
        flameLight.range = percentage * maxRange;
        flameLight.intensity = percentage * maxIntensity;
    }


    IEnumerator SlowlyKillTheFlame()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            TakeDamage(1);
        }
    }
}
