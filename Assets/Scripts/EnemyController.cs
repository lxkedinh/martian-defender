using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, objectToFollow.transform.position);
        Vector2 direction = objectToFollow.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, objectToFollow.transform.position, speed * Time.deltaTime);
    }
}
