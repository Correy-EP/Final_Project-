using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRando : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        List<GameObject> availableSpawnPoints = new List<GameObject>(propSpawnPoints);

        foreach (GameObject prefab in propPrefabs)
        {
            if (availableSpawnPoints.Count == 0) break;

            int randIndex = Random.Range(0, availableSpawnPoints.Count);
            GameObject spawnPoint = availableSpawnPoints[randIndex];
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);

            // Remove the used spawn point
            availableSpawnPoints.RemoveAt(randIndex);
        }
    }
}

