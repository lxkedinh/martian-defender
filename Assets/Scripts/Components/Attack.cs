using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    public int attackDamage;
    public UnityEvent attackEvent;

    public void PerformAttack()
    {
        attackEvent.Invoke();
    }
}
