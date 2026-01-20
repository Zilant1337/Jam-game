using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform enemyWorldParent;
    [SerializeField]
    List<Transform> enemyPrefabs;
    [SerializeField]
    float spawnOffset;
    [SerializeField]
    float spawnCooldown;
    float spawnTimer;
    bool readyToSpawn;
    [SerializeField]
    List<int> spawnPattern;
    int spawnId;

    private void Update()
    {
        if (!readyToSpawn)
        {
            spawnTimer += Time.deltaTime;
        }
        if (spawnTimer >= spawnCooldown)
        {
            readyToSpawn = true;
            spawnTimer = 0;
        }
    }
    private void Awake()
    {
        spawnId = 0;
        readyToSpawn = true;
        spawnTimer = 0;
    }
    public EnemyScript Spawn()
    {
        if (readyToSpawn)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(0, spawnOffset), 0, Random.Range(0, spawnOffset));
            if (CameraManager.Instance.CheckObjectVisibility(spawnPos))
            {
                return null;
            }
            var newEnemy = Instantiate(enemyPrefabs[spawnId], spawnPos, Quaternion.identity,enemyWorldParent);
            spawnId = (spawnId+1)%enemyPrefabs.Count;
            readyToSpawn=false;
            return newEnemy.GetComponent<EnemyScript>();
        }
        return null;
    }
}
