public class SingleUseEnemySpawner : EnemySpawner
{
    public override EnemyScript Spawn()
    {
        if (spawnId > spawnPattern.Count)
        {
            return null;
        }
        else
        {
            return base.Spawn();
        }
    }
}
