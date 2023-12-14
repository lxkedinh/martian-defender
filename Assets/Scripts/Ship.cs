using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hitbox))]
public class Ship : MonoBehaviour
{
    public Health health;
    public TMP_Text healthUI;

    // Update is called once per frame
    void Update()
    {
        healthUI.text = $"Ship <sprite name=\"heart\"> {health.health}";
    }

    public void OnDeath()
    {
        Destroy(healthUI);
    }
}
