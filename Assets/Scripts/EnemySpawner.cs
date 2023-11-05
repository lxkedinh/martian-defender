using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Vector2 spawnRange = new Vector2(3f, 3f);

    public int numEnemies = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Any update logic
    }

    public void spawnEnemies(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnerPos = transform.position;

            float offsetX = UnityEngine.Random.Range(-spawnRange.x, spawnRange.x);
            float offsetY = UnityEngine.Random.Range(-spawnRange.y, spawnRange.y);

            Vector3 randomPos = new Vector3(spawnerPos.x + offsetX, spawnerPos.y + offsetY, spawnerPos.z);
            GameObject newEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }


}
