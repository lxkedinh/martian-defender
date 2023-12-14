using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hitbox))]
public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }

    public Health health;
    public TMP_Text healthUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = $"Ship <sprite name=\"heart\"> {health.health}";
    }

    public void OnDeath()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GameStateManager.Instance.SetGameState(GameState.Death);
    }
}
