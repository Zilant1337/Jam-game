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
    protected List<string> tagsToHit;
    [SerializeField]
    protected List<string> tagsToIgnore;


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
            isUpToStartingSpeed = true;
            return;
        }
        ProgressSelfDestructTimer();
        if (currentSpeed < maxProjectileSpeed)
        {
            projectileRigidbody.AddRelativeForce(Vector3.forward * acceleration, ForceMode.Acceleration);
        }
        
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        OnCollision(other);
    }
    public virtual void OnCollision(Collider other)
    {
        bool explode = false;
        foreach (string tag in tagsToHit)
        {
            if (other.gameObject.CompareTag(tag))
            {
                explode = true;
                break;
            }
        }
        if (explode)
        {
            foreach (string tag2 in tagsToIgnore)
            {
                if (other.gameObject.CompareTag(tag2))
                {
                    explode = false;
                    break;
                }
            }
        }
        if (explode)
        {
            Debug.Log($"Collided with {other.gameObject.name}");
            isMoving = false;
            Explode(other);
            return;
        }
    }
    protected virtual void Explode(Collider collision)
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
    protected bool ToDamage(Health other)
    {
        bool toDamage = false;
        foreach (string tag in tagsToHit)
        {
            if (other.gameObject.CompareTag(tag))
            {
                toDamage = true;
                break;
            }
        }
        if (toDamage)
        {
            foreach (string tag2 in tagsToIgnore)
            {
                if (other.gameObject.CompareTag(tag2))
                {
                    toDamage = false;
                    break;
                }
            }
        }
        return toDamage;
    }
    protected virtual List<Health> GetHealthsToDamage(Collider collision)
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
                if(ToDamage(health))
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
