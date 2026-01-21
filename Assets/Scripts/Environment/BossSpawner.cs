using UnityEngine;

public class BossSpawner : SingleUseEnemySpawner
{
    [SerializeField]
    ConditionalDoorOpener bossDoorOpener;
    public override EnemyScript Spawn()
    {
        if (spawnId > spawnPattern.Count)
        {
            return null;
        }
        else
        {
            if (readyToSpawn)
            {
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(0, spawnOffset), 0, Random.Range(0, spawnOffset));
                if (CameraManager.Instance.CheckObjectVisibility(spawnPos))
                {
                    return null;
                }
                var newEnemy = Instantiate(enemyPrefabs[spawnId], spawnPos, Quaternion.identity, enemyWorldParent);
                readyToSpawn = false;
                BossScript bossScript = newEnemy.GetComponent<BossScript>();
                if (!bossScript)
                {
                    Destroy(newEnemy.gameObject);
                    Debug.LogError("Can't spawn a normal enemy with a boss spawner");
                    return null;
                }
                if(bossDoorOpener)
                {
                    bossScript.BossDoorOpenerHelper.ReplaceDoorOpener(bossDoorOpener);
                    bossDoorOpener.AddHelper(bossScript.BossDoorOpenerHelper);
                }
                spawnId++;
                return bossScript;
            }
            return null;
        }
    }
}
