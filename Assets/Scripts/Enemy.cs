using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hitbox))]
public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float moveSpeed;
    public Transform target;
    private float distance;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ship").transform;
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Keeps NavMesh from rotating Enemy
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            distance = Vector2.Distance(transform.position, target.position);
            Vector2 direction = target.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == target)
        {
            Destroy(gameObject);
        }
    }
}
