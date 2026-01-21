using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    protected Transform enemyWorldParent;
    [SerializeField]
    protected List<Transform> enemyPrefabs;
    [SerializeField]
    protected float spawnOffset;
    [SerializeField]
    protected float spawnCooldown;
    protected float spawnTimer;
    protected bool readyToSpawn;
    [SerializeField]
    protected List<int> spawnPattern;
    protected int spawnId;

    protected virtual void Update()
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
    protected virtual void Awake()
    {
        spawnId = 0;
        readyToSpawn = true;
        spawnTimer = 0;
    }
    public virtual EnemyScript Spawn()
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
