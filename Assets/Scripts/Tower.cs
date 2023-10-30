using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour
{
    public int rangeRadius;
    private bool isSelected;
    public GameObject rangeIndicator;
    public GameObject outlineIndicator;
    public GameObject firePoint;
    public Projectile projectilePrefab;
    public float attackCooldown = 1.5f; // seconds
    private float nextAttack = 0.0f;
    private List<Enemy> enemiesInRange = new();
    public Enemy target = null;

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
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemiesInRange.Add(enemy);
            Debug.Log(enemiesInRange.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == null) return;

        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Enemy enemyLeavingRange = enemiesInRange.Find(e => e == enemy);
            if (enemyLeavingRange != null)
            {
                enemiesInRange.Remove(enemyLeavingRange);
            }
            Debug.Log(enemiesInRange.Count);
        }
    }
}
