using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NuggetManager : MonoBehaviour
{
    public Nugget nuggetPrefab;
    public float minSpawnTime;
    public float maxSpawnTime;
    
    // state
    List<Transform> spawnPoints;
    List<Transform> unusedSpawnPoints;
    float randomTimeUntilNextSpawn;
    int nuggetCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize list of spawn point objects. They should be children of the nugget manager
        spawnPoints = new List<Transform>();
        unusedSpawnPoints = spawnPoints.ToList();
        Transform spawnPointParent = transform.Find("NuggetSpawners");
        int spawnPointCount = spawnPointParent.childCount;
        for (int i = 0; i < spawnPointCount; i++) {
            spawnPoints.Add(spawnPointParent.GetChild(i));
        }


        randomTimeUntilNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        // only update the nugget spawn timer if there is not already a spawned nugget available for pickup
        if (nuggetCount == 0) {
            randomTimeUntilNextSpawn -= Time.deltaTime;
        }
        
        // if the spawn timer runs out, spawn a nugget at at randomly chosen spawn point
        if (randomTimeUntilNextSpawn <= 0) {
            int randomIndex = Random.Range(0, unusedSpawnPoints.Count - 1);
            AddNuggetAtPosition(unusedSpawnPoints[randomIndex].position);
            unusedSpawnPoints.RemoveAt(randomIndex);
            randomTimeUntilNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }

        if (unusedSpawnPoints.Count <= 0) {
            unusedSpawnPoints = spawnPoints.ToList();
        }
    }

    public void AddNuggetAtPosition(Vector3 position) {
        Instantiate(nuggetPrefab, position, Quaternion.identity, transform);
        nuggetCount++;
    }

    public void DestroyNugget(GameObject nugget) {
        Destroy(nugget);
        nuggetCount--;
    }
}
