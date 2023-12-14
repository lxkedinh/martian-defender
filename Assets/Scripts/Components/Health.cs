using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public UnityEvent onDeath;
    public UnityEvent onTakeDamage;

    public void TakeDamage(Attack attack)
    {
        health -= attack.attackDamage;
        onTakeDamage?.Invoke();
        if (health <= 0)
        {
            onDeath?.Invoke();
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
}
