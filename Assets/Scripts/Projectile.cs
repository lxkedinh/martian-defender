using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 fireDirection;
    public Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity = fireDirection * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
