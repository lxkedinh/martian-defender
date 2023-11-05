using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float moveSpeed;
    public Transform target;
    public NavMeshAgent agent;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ship").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new(0f, -1f, 0f);
        rbody.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * movement);

        // NavMeshPath path = new NavMeshPath();
        // agent.CalculatePath(target.position, path);

        // if (path.status == NavMeshPathStatus.PathComplete)
        // {
        //     UnityEngine.Debug.Log("Has Path: " + agent.hasPath);
        //     agent.SetDestination(target.position);
        // }
        // else
        // {
        //     distance = Vector2.Distance(transform.position, target.position);
        //     Vector2 direction = target.position - transform.position;

        //     transform.position = Vector2.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
        // }
    }
}
