using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOreGenerator : MonoBehaviour
{
    public GameObject copperOrePrefab;
    public GameObject ironOrePrefab;

    private int minXBound = -10; 
    private int maxXBound = 10;  

    private int minYBound = -10; 
    private int maxYBound = 10;

    private int minZBound = -10; 
    private int maxZBound = 10; 

    public int minNumberOfOres = 10;
    public int maxNumberOfOres = 20;

    public float oreSpawnRadius = 1.0f; // Adjust this radius as needed for clearance.

    public bool hasCollision;

    private void Start()
    {
        SpawnOres();
    }

    private void SpawnOres()
    {
        int numberOfOresToSpawn = Random.Range(minNumberOfOres, maxNumberOfOres + 1);

        for (int i = 0; i < numberOfOresToSpawn; i++)
        {
            Vector3 randomSpawnPosition;
            GameObject orePrefabToSpawn;

            int maxAttempts = 10;
            int currentAttempt = 0;

            do
            {
                randomSpawnPosition = new Vector3(
                    Random.Range(minXBound, maxXBound),
                    Random.Range(minYBound, maxYBound),
                    Random.Range(minZBound, maxZBound)
                );

                orePrefabToSpawn = Random.Range(0f, 1f) < 0.5f ? copperOrePrefab : ironOrePrefab;

                bool hasCollision = Physics.CheckSphere(randomSpawnPosition, oreSpawnRadius);

                currentAttempt++;
                if (currentAttempt >= maxAttempts)
                {
                    Debug.LogWarning("Max spawn attempts reached. Unable to spawn an ore without collision.");
                    break;
                }
            }
            while (hasCollision);

            Instantiate(orePrefabToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}