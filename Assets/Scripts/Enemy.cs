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

    public float damageInterval = 1f;
    private float nextDamageTime = 0f;

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

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall") && Time.time >= nextDamageTime)
        {
            DealDamageToWall(collider);
            nextDamageTime = Time.time + damageInterval; // Sets new damage time
        }
    }

    void DealDamageToWall(Collider2D collider)
    {
        Health wallHealth = collider.gameObject.GetComponent<Health>();
        Attack enemyAttack = GetComponent<Attack>();
        if (wallHealth != null)
        {
            wallHealth.TakeDamage(enemyAttack);
        }
    }
}
