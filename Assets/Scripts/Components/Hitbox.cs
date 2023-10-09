using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent<Health>(out Health health) && other.TryGetComponent<Attack>(out Attack attack))
        {
            health.TakeDamage(attack);
        }
    }
}
