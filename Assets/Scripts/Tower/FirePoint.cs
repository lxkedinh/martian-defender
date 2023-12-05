using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public TargetController targetController;
    public float rotationSpeed;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        ChangeSprite(transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardsEnemy(targetController.TargetedEnemy);
    }

    void RotateTowardsEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        Vector3 targetVec = (enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetVec.y, targetVec.x) * Mathf.Rad2Deg - rotationSpeed;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
        Debug.Log(transform.rotation.eulerAngles.z);
    }

    void ChangeSprite(float z)
    {

        switch (z)
        {
            case > 337.5f:
            case <= 22.5f:
                spriteRenderer.sprite = sprites[0];
                break;
            case > 22.5f when z <= 67.5f:
                spriteRenderer.sprite = sprites[1];
                break;
            case > 67.5f when z <= 112.5f:
                spriteRenderer.sprite = sprites[2];
                break;
            case > 112.5f when z <= 157.5f:
                spriteRenderer.sprite = sprites[3];
                break;
            case > 157.5f when z <= 202.5f:
                spriteRenderer.sprite = sprites[4];
                break;
            case > 202.5f when z <= 247.5f:
                spriteRenderer.sprite = sprites[5];
                break;
            case > 247.5f when z <= 292.5f:
                spriteRenderer.sprite = sprites[6];
                break;
            case > 292.5f when z <= 337.5f:
                spriteRenderer.sprite = sprites[7];
                break;
        }
    }
}
