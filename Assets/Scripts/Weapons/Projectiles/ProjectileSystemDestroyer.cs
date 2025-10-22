using UnityEngine;

public class ProjectileSystemDestroyer : MonoBehaviour
{
    [SerializeField]
    protected float timeToLive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyProjectileSystem()
    {
        Destroy(gameObject,timeToLive);
    }
}
