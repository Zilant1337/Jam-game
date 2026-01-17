using UnityEngine;
using UnityEngine.ProBuilder;


public class ProjectilePattern : BulletPattern
{
    [SerializeField]
    protected Transform projectileRangeIndicatorPrefabTransform;
    [SerializeField]
    protected Transform projectilePrefabTransform;
    [SerializeField]
    protected float projectileSpawnDistance;
    protected override void ShootNextVolley()
    {
        Vector3 direction = transform.forward;
        Quaternion rotation = Quaternion.AngleAxis(volleyAngles[volleyCounter], Vector3.up);
        direction = rotation * direction;
        direction = direction.normalized;
        var projectile = Instantiate(projectilePrefabTransform, transform.position + direction * projectileSpawnDistance, Quaternion.LookRotation(direction, Vector3.up));
        float explosionRadius = 0;
        if (projectilePrefabTransform.GetComponent<ExplosiveRocket>())
            explosionRadius = projectilePrefabTransform.GetComponent<ExplosiveRocket>().ExplosionRadius;

        if (projectileRangeIndicatorPrefabTransform)
        {
            var rangeIndicator = Instantiate(projectileRangeIndicatorPrefabTransform, transform.position, Quaternion.identity);
            rangeIndicator.GetComponent<RangeIndicator>().SetSize(explosionRadius);
            rangeIndicator.GetComponent<RangeIndicator>().ObjectToFollow = projectile.transform;
        }

        volleyCounter++;
    }
}
