using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Rocket : Projectile
{
    [SerializeField]
    protected float maxProjectileSpeed;
    [SerializeField]
    protected float startingSpeed;
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

    [SerializeField]
    protected AudioSource moveSound;
    [SerializeField]
    protected AudioSource explodeSound;
    [SerializeField]
    protected float explodeSoundPitchRange;


    protected bool canDamage;
    protected bool isUpToStartingSpeed;
    protected bool isMoving;

    protected override void Move()
    {
        if (!isMoving)
        {
            projectileRigidbody.linearVelocity = Vector3.zero;
            return;
        }
        currentSpeed = projectileRigidbody.linearVelocity.magnitude;
        if (!isUpToStartingSpeed)
        {
            projectileRigidbody.AddRelativeForce(Vector3.forward * startingSpeed, ForceMode.VelocityChange);
            Debug.Log($"Added {Vector3.forward * startingSpeed} of speed to projectile to get it going. Actual velocity = {projectileRigidbody.linearVelocity}");
            isUpToStartingSpeed = true;
            return;
        }
        selfDestructTimer += Time.deltaTime;
        if(selfDestructTimer>= timeToSelfDestruct)
        {
            Destroy(gameObject);
            return;
        }
        if (currentSpeed < maxProjectileSpeed)
        {
            projectileRigidbody.AddRelativeForce(Vector3.forward * acceleration, ForceMode.Acceleration);
            Debug.Log($"Adding {acceleration} force, Projectile speed = {currentSpeed}");
        }
        
    }
    protected void OnCollisionEnter(Collision collision)
    {
        isMoving = false;
        Explode(collision);
    }
    protected virtual void Explode(Collision collision)
    {
        rocketExplosionParticleSystem.transform.SetParent(null);
        rocketExplosionParticleSystem.Play();
        if(explodeSound!=null)
        {
            explodeSound.transform.SetParent(null);
            explodeSound.pitch += Random.Range(-explodeSoundPitchRange,explodeSoundPitchRange);
            explodeSound.PlayOneShot(explodeSound.clip);
        }
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
        canDamage = true;
        isUpToStartingSpeed = false;
        isMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
