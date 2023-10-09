using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    void Start()
    {

    }

    public void TakeDamage(Attack attack)
    {
        health -= attack.attackDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
