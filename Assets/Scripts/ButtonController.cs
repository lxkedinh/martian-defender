using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        enemySpawner.spawnEnemies(10);
        Destroy(gameObject);
    }
}


