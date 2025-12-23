using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerArea : MonoBehaviour
{
    [SerializeField]
    List<EnemySpawner> enemySpawners;
    public bool Spawn()
    {
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            EnemyScript enemy = enemySpawner.Spawn();
            if (enemy)
            {
                enemy.EnemySpawnerArea = this;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterScript playerCharacter = other.GetComponent<CharacterScript>();
        if (playerCharacter)
        {
            EnemyManager.instance.CurrentSpawnerArea = this;
        }
    }
}
