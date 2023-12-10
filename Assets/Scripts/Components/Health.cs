using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    
    }


    public void TakeDamage(Attack attack)
    {
        currentHealth -= attack.attackDamage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            if (gameObject.CompareTag("Wall"))
            {
                NavMeshSurface Surface2D = FindObjectOfType<NavMeshSurface>();
                if (Surface2D != null)
                {
                    Surface2D.BuildNavMesh();
                }
            }
        }
    }
}
