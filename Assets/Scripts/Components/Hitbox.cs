using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (TryGetComponent<Health>(out Health health) && collider.TryGetComponent<Attack>(out Attack attack) && (gameObject.CompareTag("Ship") || !collider.gameObject.CompareTag("Enemy")))
        {
            health.TakeDamage(attack);
        }
    }
}
