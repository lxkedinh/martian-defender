using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour
{
    public static int rangeRadius = 5;
    public bool isSelected;
    public Outline outline;
    public GameObject rangeIndicator;
    public float attackCooldown = 1.5f; // seconds
    private float nextAttack = 0.0f;
    public static int buildCost = 3;
    public static bool CanBuild
    {
        get
        {
            return InventoryController.Instance.inventory[Materials.Copper] >= buildCost;
        }
    }
    public TargetController targetController;
    public FirePoint firePoint;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = true;
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
        outline.ShowOutline();
    }

    public void Deselect()
    {
        isSelected = false;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = false;
        outline.HideOutline();
    }

    public void FireAttack()
    {
        if (targetController.TargetedEnemy == null) return;

        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            firePoint.SpawnProjectile();
        }
    }
}
