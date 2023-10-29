using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 velocity;
    public Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity = transform.right * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
