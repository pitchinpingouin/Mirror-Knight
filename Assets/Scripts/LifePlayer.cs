using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : LifeManager
{
    [SerializeField] private int slowFlameDamage = 1;
    [SerializeField] private float timeBeforeFlameDamage = 1.0f;

    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;


    private GameObject deathlight;
    private GameObject torch;


    private Light flameLight;
    private GameObject torchGameObject;

    // Update is called once per frame
    override protected void Start()
    {
        base.Start();
        deathlight = transform.Find("deathLight").gameObject;
        torch = transform.Find("torch").gameObject;
        isGoingToDieNextHit = false;
        torchGameObject = transform.Find("torch").gameObject;
        flameLight = torchGameObject.GetComponentInChildren<Light>();
        StartCoroutine("SlowlyKillTheFlame");
    }

    private void Update()
    {
        float percentage = (float)currentHealth / (float)maxHealth;
        flameLight.range = percentage * maxRange + minRange;
        flameLight.intensity = percentage * maxIntensity + minIntensity;
    }


    IEnumerator SlowlyKillTheFlame()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBeforeFlameDamage);
            TakeDamage(slowFlameDamage);
        }
    }

    public override void TakeDamage(int damageOrHeal)
    {
        if (isGoingToDieNextHit)
        {
            if (damageOrHeal > slowFlameDamage)
            {
                Die();
            }
        }

        {
            if (currentHealth - damageOrHeal > maxHealth)
            {
                currentHealth = maxHealth;
            }

            currentHealth -= damageOrHeal;

            if (currentHealth <= 0)
            {
                torch.SetActive(false);
                deathlight.SetActive(true);
                isGoingToDieNextHit = true;
            }
            else
            {
                torch.SetActive(true);
                deathlight.SetActive(false);
                isGoingToDieNextHit = false;
            }
        }

    }
}
