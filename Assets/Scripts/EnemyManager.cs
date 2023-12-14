using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public Enemy enemyPrefab;
    public HashSet<Enemy> spawnedEnemies;

    public Vector2 spawnRange = new(3f, 3f);

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

    void Start()
    {
        spawnedEnemies = new();
    }

    void Update()
    {
        if (DayNightController.Instance.cyclePhase == TimeOfDay.Night && spawnedEnemies.Count <= 0)
        {
            GameStateManager.Instance.SetGameState(GameState.Win);
        }
    }

    public void SpawnEnemies(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnerPos = transform.position;

            float offsetX = UnityEngine.Random.Range(-spawnRange.x, spawnRange.x);
            float offsetY = UnityEngine.Random.Range(-spawnRange.y, spawnRange.y);

            Vector3 randomPos = new(spawnerPos.x + offsetX, spawnerPos.y + offsetY, spawnerPos.z);
            Enemy newEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);

            // Keeps NavMesh from rotating Enemy
            NavMeshAgent agent = newEnemy.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        spawnedEnemies.Remove(enemy);
    }


}
