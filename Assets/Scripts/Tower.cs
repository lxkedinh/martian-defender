using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int range;
    public GameObject rangeIndicator;

    // Start is called before the first frame update
    void Start()
    {
        range = Random.Range(3, 5);
        float scaleFactor = (2 * range) + 1f;
        rangeIndicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void Select()
    {
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deselect()
    {
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
