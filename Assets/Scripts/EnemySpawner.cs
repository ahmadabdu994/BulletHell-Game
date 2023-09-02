using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnList
    {
        public string listName;
        public GameObject[] spawnableObjects;
        public Transform spawnPosition;
        public Vector2 spawnRangeX;
        public Vector2 spawnRangeY;
        public float spawnInterval; // New field for spawn interval
    }

    public List<SpawnList> spawnLists = new List<SpawnList>();

    private void Start()
    {
        foreach (SpawnList spawnList in spawnLists)
        {
            StartCoroutine(SpawnWithInterval(spawnList));
        }
    }

    private IEnumerator SpawnWithInterval(SpawnList spawnList)
    {
        while (true)
        {
            Spawn(spawnList);
            yield return new WaitForSeconds(spawnList.spawnInterval);
        }
    }

    private void Spawn(SpawnList spawnList)
    {
        int randomIndex = Random.Range(0, spawnList.spawnableObjects.Length);
        GameObject selectedObject = spawnList.spawnableObjects[randomIndex];

        float randomX = Random.Range(spawnList.spawnPosition.position.x + spawnList.spawnRangeX.x,
                                     spawnList.spawnPosition.position.x + spawnList.spawnRangeX.y);

        float randomY = Random.Range(spawnList.spawnPosition.position.y + spawnList.spawnRangeY.x,
                                     spawnList.spawnPosition.position.y + spawnList.spawnRangeY.y);

        // Ignore the Z-axis component by using the original Z position
        Vector3 spawnPosition = new Vector3(randomX, randomY, spawnList.spawnPosition.position.z);

        Instantiate(selectedObject, spawnPosition, Quaternion.identity);
    }
}