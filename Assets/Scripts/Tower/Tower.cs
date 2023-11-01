using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int rangeRadius;
    private bool isSelected;
    public GameObject rangeIndicator;
    public GameObject outlineIndicator;
    public GameObject firePoint;
    public Projectile projectilePrefab;
    public float attackCooldown = 1.5f; // seconds
    private float nextAttack = 0.0f;
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
        ShowOutline();
    }

    public void ShowOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Deselect()
    {
        isSelected = false;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = false;
        HideOutline();
    }

    public void FireAttack()
    {
        if (TargetedEnemy == null) return;

        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            Projectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
            Vector2 fireDirection = (TargetedEnemy.transform.position - transform.position).normalized;
            projectile.fireDirection = fireDirection;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideOutline();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MapController.Instance.SelectTower(this);
    }
}
