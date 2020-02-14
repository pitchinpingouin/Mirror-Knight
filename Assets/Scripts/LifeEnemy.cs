using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : LifeManager
{
    protected override void Die()
    {
        currentHealth = maxHealth;
    }
}
