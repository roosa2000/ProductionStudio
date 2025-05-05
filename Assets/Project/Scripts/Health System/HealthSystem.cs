using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int health;
    private int maxHealth;
    public static HealthSystem Instance;
    
    public HealthSystem(int maxHealth)
    {
        Instance = this;
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int getHealth()
    {
        return health;
    }

    public void Damage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public float GetHealthPercentage()
    {
        return (float)health / maxHealth;
    }
}
