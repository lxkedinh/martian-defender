using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour
{
    public int rangeRadius;
    private bool isSelected;
    public TowerBody towerBody;
    public GameObject rangeIndicator;
    public GameObject firePoint;
    public Projectile projectilePrefab;
    public float attackCooldown = 1.5f; // seconds
    private float nextAttack = 0.0f;
    public TargetController targetController;

    // Start is called before the first frame update
    void Start()
    {
        rangeRadius = 4;
        float scaleFactor = (2 * rangeRadius) + 1f;
        rangeIndicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    void Update()
    {
        FireAttack();
    }

    public void Select()
    {
        isSelected = true;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = true;
        towerBody.ShowOutline();
    }

    public void Deselect()
    {
        isSelected = false;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = false;
        towerBody.HideOutline();
    }

    public void FireAttack()
    {
        if (targetController.TargetedEnemy == null) return;

        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            Projectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
            Vector2 fireDirection = (targetController.TargetedEnemy.transform.position - transform.position).normalized;
            projectile.fireDirection = fireDirection;
        }
    }
}
