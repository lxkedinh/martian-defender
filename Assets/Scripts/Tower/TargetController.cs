using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private readonly List<Enemy> enemiesInRange = new();
    // intermediate reference variable to determine the targetedEnemy in its getter method
    private Enemy targetReference = null;

    public Enemy TargetedEnemy
    {
        get
        {
            if (targetReference == null)
            {
                // clean up null enemy references due to gameobjects being destroyed after tower kills them
                enemiesInRange.RemoveAll(enemy => enemy == null);

                if (enemiesInRange.Count > 0)
                {
                    targetReference = enemiesInRange.Find(enemy => enemy);
                }
            }

            return targetReference;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == null) return;

        if (collider.TryGetComponent(out Enemy enemy))
        {
            Enemy enemyLeavingRange = enemiesInRange.Find(e => e == enemy);
            if (enemyLeavingRange != null)
            {
                enemiesInRange.Remove(enemyLeavingRange);
            }

            if (enemy == targetReference)
            {
                targetReference = null;
            }
        }
    }
}
