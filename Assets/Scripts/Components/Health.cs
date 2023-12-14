using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public UnityEvent onDeath;

    public void TakeDamage(Attack attack)
    {
        health -= attack.attackDamage;
        if (health <= 0)
        {
            onDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
