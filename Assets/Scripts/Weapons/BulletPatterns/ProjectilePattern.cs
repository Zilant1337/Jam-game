using UnityEngine;
using UnityEngine.UIElements;

public class ProjectilePattern : BulletPattern
{
    [SerializeField]
    protected Transform projectilePrefabTransform;
    protected override void ShootNextVolley()
    {
        Vector3 direction = transform.forward;
        Quaternion rotation = Quaternion.AngleAxis(volleyAngles[volleyCounter], Vector3.up);
        direction = rotation * direction;
        direction = direction.normalized;
        Instantiate(projectilePrefabTransform, transform.position, Quaternion.LookRotation(direction, Vector3.up));
        volleyCounter++;
        timer = volleyDelay[volleyCounter];
    }
}
