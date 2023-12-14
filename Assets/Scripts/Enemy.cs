using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Animator animator;
    public static float attackCooldown = 1.5f;
    public static float nextAttack = 0f;
    public bool isAttacking;

    NavMeshAgent agent;
    NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        target = Ship.Instance.transform;
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Keeps NavMesh from rotating Enemy
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        path = new NavMeshPath();
        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameStateManager.Instance.currentState != GameState.Playing) return;

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        NavMeshPath path = new();
        agent.CalculatePath(target.position, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            distance = Vector2.Distance(transform.position, target.position);
            Vector2 direction = target.position - transform.position;

            if (!isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void OnTakeDamage(Attack attack)
    {
        GetComponent<Health>().TakeDamage(attack);
        animator.SetTrigger("wasHit");
    }

    public void OnDeath()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        animator.SetTrigger("hasDied");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == target)
        {
            EnemyManager.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            AttackWall(collider.GetComponent<Wall>());
        }
    }

    public void AttackWall(Wall wall)
    {
        isAttacking = true;
        if (Time.time < nextAttack) return;

        nextAttack = Time.time + attackCooldown;
        animator.SetTrigger("isAttacking");
        var wallHealth = wall.GetComponent<Health>();
        Attack enemyAttack = GetComponent<Attack>();
        if (wallHealth != null)
        {
            wallHealth.TakeDamage(enemyAttack);
        }
    }
}
