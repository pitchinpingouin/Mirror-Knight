using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    virtual protected void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageOrHeal)
    {
        if(currentHealth - damageOrHeal > maxHealth)
        {
            currentHealth = maxHealth;
        }

        currentHealth -= damageOrHeal;

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

}
