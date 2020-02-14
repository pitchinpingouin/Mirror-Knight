using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeShield : LifeManager
{
    [SerializeField] private float timeToRepair;
    [SerializeField] private float timeToHeal;
    [SerializeField] private int healPerFrame; //Must be positive
    private float timerRepair;
    private float timerHeal;
    private Light spotlight;

    private Color newMatColor;
    private Color newMatEmission;


    public Color c_fullHealth;
    public Color c_zeroHealth;
    public Color e_fullHealth;
    public Color e_zeroHealth;

    MeshRenderer mRenderer;
    BoxCollider bCollider;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spotlight = GetComponentInChildren<Light>();
        mRenderer = GetComponent<MeshRenderer>();
        bCollider = GetComponent<BoxCollider>();

        SetColors();
    }


    private void SetColors()
    {
        newMatEmission = Color.Lerp(e_zeroHealth, e_fullHealth, (float)currentHealth / (float)maxHealth);
        newMatColor = Color.Lerp(c_zeroHealth, c_fullHealth, (float)currentHealth / (float)maxHealth);

        spotlight.color = newMatColor;

        mRenderer.material.SetColor("_EmissionColor", newMatEmission);
        mRenderer.material.SetColor("_Color", newMatColor);
    }

    //TODO: Regenerate shield Health

    IEnumerator SlowlyHealsIfNoBreak()
    {
        timerHeal = 0.0f;

        while (timerHeal < timeToHeal)
        {
            timerHeal += Time.deltaTime;
            yield return null;
        }

        while (currentHealth < maxHealth)
        {
            TakeDamage(-healPerFrame);
            yield return null;
        }
        
    }

    IEnumerator WaitBeforeRepair()
    {
        timerRepair = 0.0f;

        while(timerRepair < timeToRepair)
        {
            timerRepair += Time.deltaTime;
            yield return null;
        }

        currentHealth = maxHealth;

        SetColors();

        bCollider.enabled = true;
        mRenderer.enabled = true;
        spotlight.gameObject.SetActive(true);
    }

    public override void TakeDamage(int damageOrHeal)
    {
        base.TakeDamage(damageOrHeal);

        SetColors();

        if(damageOrHeal > 0 && currentHealth > 0)
        {
            StopCoroutine("SlowlyHealsIfNoBreak");
            StartCoroutine("SlowlyHealsIfNoBreak");
        }
        
    }

    // Update is called once per frame
    protected override void Die()
    {
        mRenderer.enabled = false;
        bCollider.enabled = false;
        spotlight.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine("WaitBeforeRepair");
    }
}
