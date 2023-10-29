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
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        }
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

    }
}
