using UnityEngine;

public class ProjectilePattern : BulletPattern
{
    [SerializeField]
    protected Transform projectilePrefabTransform;
    public override void ShootNextVolley()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
