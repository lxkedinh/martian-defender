using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
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
    private Transform closestWall;

    private Animator animator;

    NavMeshAgent agent;

    NavMeshPath path;

    public float damageInterval = 1f;
    private float nextDamageTime = 0f;

    public bool isStopped = false;
    public bool isAttacking = false;

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

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Transform getClosestWall()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in walls)
        {
            float currentDistance = Vector3.Distance(transform.position, go.transform.position);

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        path = new NavMeshPath();
        agent.CalculatePath(target.position, path);

        if (!isStopped)
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            else
            {
                distance = Vector2.Distance(transform.position, target.position);
                Vector2 direction = target.position - transform.position;

                transform.position = Vector2.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        } else
        {
            agent.isStopped = true;
        }

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            isStopped = false;
        }

        if (isAttacking && Time.time >= nextDamageTime)
        {
            isStopped = true;
            DealDamageToWall(getClosestWall().GetComponent<Collider2D>());
            nextDamageTime = Time.time + damageInterval; // Sets new damage time
            animator.SetTrigger("isAttacking");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == target)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            isAttacking = false;
            isStopped = false;
        }

        if (collider.CompareTag("Enemy"))
        {
            Enemy otherEnemy = collider.GetComponent<Enemy>();
            if (otherEnemy != null && !otherEnemy.isAttacking)
            {
                isStopped = false;
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            if (collider.gameObject.CompareTag("Wall"))
            {
                isAttacking = true;
                isStopped = true;
            }

            if (collider.CompareTag("Enemy"))
            {
                Enemy otherEnemy = collider.GetComponent<Enemy>();
                if (otherEnemy != null && otherEnemy.isAttacking)
                {
                    isStopped = true;
                }
            }
        }
    }

    void DealDamageToWall(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            Health wallHealth = collider.gameObject.GetComponent<Health>();
            Attack enemyAttack = GetComponent<Attack>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(enemyAttack);
            }
        }
    }
    
    public void Dead()
    {
        Destroy(gameObject);
    }

}
