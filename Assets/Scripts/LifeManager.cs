using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeManager : MonoBehaviour
{
    [SerializeField] public int maxHealth;

    public bool isGoingToDieNextHit;

    public int currentHealth
    {
        get;
        protected set;
    }

    virtual protected void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damageOrHeal)
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

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

}
