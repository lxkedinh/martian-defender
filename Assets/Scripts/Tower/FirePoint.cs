using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public TargetController targetController;
    public float rotationSpeed;
    public Outline outline;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public Projectile projectilePrefab;

    void Start()
    {
        InvokeRepeating(nameof(RotateTowardsEnemy), 0, 0.05f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeSprite(transform.rotation.eulerAngles.z);
    }

    void RotateTowardsEnemy()
    {
        Enemy enemy = targetController.TargetedEnemy;
        if (enemy == null) return;

        Vector3 targetVec = (enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetVec.y, targetVec.x) * Mathf.Rad2Deg - rotationSpeed;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
    }

    void ChangeSprite(float z)
    {
        z = (float)Math.Round(z, 1);

        switch (z)
        {
            case > 337.5f:
            case <= 22.5f:
                spriteRenderer.sprite = sprites[0];
                outline.ChangeOutline(0);
                transform.localPosition = new(0.3f, 0.3f, 0);
                break;
            case > 22.5f when z <= 67.5f:
                spriteRenderer.sprite = sprites[1];
                outline.ChangeOutline(1);
                transform.localPosition = new(0.2f, 0.5f, 0);
                break;
            case > 67.5f when z <= 112.5f:
                spriteRenderer.sprite = sprites[2];
                outline.ChangeOutline(2);
                transform.localPosition = new(0f, 0.55f, 0);
                break;
            case > 112.5f when z <= 157.5f:
                spriteRenderer.sprite = sprites[3];
                outline.ChangeOutline(3);
                transform.localPosition = new(-0.2f, 0.5f, 0);
                break;
            case > 157.5f when z <= 202.5f:
                spriteRenderer.sprite = sprites[4];
                outline.ChangeOutline(4);
                transform.localPosition = new(-0.3f, 0.3f, 0);
                break;
            case > 202.5f when z <= 247.5f:
                spriteRenderer.sprite = sprites[5];
                outline.ChangeOutline(5);
                transform.localPosition = new(-0.2f, -0.5f, 0);
                break;
            case > 247.5f when z <= 292.5f:
                spriteRenderer.sprite = sprites[6];
                outline.ChangeOutline(6);
                transform.localPosition = new(0f, -0.55f, 0);
                break;
            case > 292.5f when z <= 337.5f:
                spriteRenderer.sprite = sprites[7];
                outline.ChangeOutline(7);
                transform.localPosition = new(0.2f, -0.5f, 0);
                break;
        }
    }

    public void SpawnProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.target = targetController.TargetedEnemy;
        projectile.GetComponent<Attack>().attackDamage = GetComponentInParent<Attack>().attackDamage;
    }
}
