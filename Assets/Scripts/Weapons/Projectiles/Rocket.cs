using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Rocket : Projectile
{
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float currentSpeed;
    [SerializeField]
    protected float acceleration;
    [SerializeField]
    protected ParticleSystem rocketParticleSystem;
    [SerializeField] 
    protected ParticleSystem rocketExplosionParticleSystem;
    [SerializeField]
    protected ProjectileSystemDestroyer explosionProjectileDestroyer;
    [SerializeField]
    protected float timeToSelfDestruct;
    protected float selfDestructTimer;
    protected bool isAccellerating;
    protected bool canDamage;

    protected override void Move()
    {
        selfDestructTimer += Time.deltaTime;
        if(selfDestructTimer>= timeToSelfDestruct)
        {
            Destroy(gameObject);
        }
        if (isAccellerating)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        if (currentSpeed > projectileSpeed)
        {
            currentSpeed = projectileSpeed;
            isAccellerating = false;
        }
        Debug.Log($"Adding {currentSpeed} force");
        projectileRigidbody.AddRelativeForce(Vector3.forward*currentSpeed*1000*Time.deltaTime,ForceMode.Acceleration);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        Explode(collision);
    }
    protected virtual void Explode(Collision collision)
    {
        rocketExplosionParticleSystem.transform.SetParent(null);
        rocketExplosionParticleSystem.Play();
        explosionProjectileDestroyer.DestroyProjectileSystem();
        List<Health> healthsToDamage = GetHealthsToDamage(collision);
        if(canDamage)
            DealDamage(healthsToDamage);
        Destroy(this.gameObject);
    }
    protected virtual List<Health> GetHealthsToDamage(Collision collision)
    {
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        return new List<Health> { enemyHealth };
    }
    protected virtual void DealDamage(List<Health> healthList)
    {
        canDamage = false;
        foreach (Health health in healthList)
        {
            if (health != null)
                health.TakeDamage(damage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfDestructTimer = 0;
        isAccellerating = true;
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
