using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new(0f, -1f, 0f);
        rbody.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * movement);

    }
}
