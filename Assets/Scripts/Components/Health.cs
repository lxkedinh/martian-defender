using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using System.Diagnostics;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void TakeDamage(Attack attack)
    {
        currentHealth -= attack.attackDamage;


        UnityEngine.Debug.Log("TakeDamage() called from: " + new System.Diagnostics.StackTrace());
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Enemy") && animator != null)
            {
                animator.SetTrigger("isDead");
            }
            else
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
            
        } else
        {
            if (gameObject.CompareTag("Enemy") && animator != null)
            {
                animator.SetTrigger("isHit");
            }
        }
    }
}
