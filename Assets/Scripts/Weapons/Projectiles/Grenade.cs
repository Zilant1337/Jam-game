using System.Collections.Generic;
using UnityEngine;

public class Grenade : ExplosiveRocket
{
    [SerializeField]
    protected Vector3 launchDirection;

    protected override void Move()
    {
        if (!isMoving)
        {
            projectileRigidbody.linearVelocity = Vector3.zero;
            return;
        }
        ProgressSelfDestructTimer();
        if (!isUpToStartingSpeed)
        {
            projectileRigidbody.AddRelativeForce(launchDirection * startingSpeed, ForceMode.VelocityChange);
            isUpToStartingSpeed = true;
            return;
        }
    }
    protected void Explode(Collision collision)
    {
        rocketExplosionParticleSystem.transform.SetParent(null);
        rocketExplosionParticleSystem.Play();
        if (explodeSound != null)
        {
            explodeSound.transform.SetParent(null);
            explodeSound.pitch += Random.Range(-explodeSoundPitchRange, explodeSoundPitchRange);
            explodeSound.PlayOneShot(explodeSound.clip);
        }
        explosionProjectileDestroyer.DestroyProjectileSystem();
        List<Health> healthsToDamage = GetHealthsToDamage(collision);
        if (canDamage)
            DealDamage(healthsToDamage);
        Destroy(this.gameObject);
    }
    protected List<Health> GetHealthsToDamage(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && dealDamageOnImpact && ToDamage(health))
        {
            healthsToDamage.Add(health);
        }
        return healthsToDamage;
    }
    protected override void ProgressSelfDestructTimer()
    {
        selfDestructTimer += Time.deltaTime;
        if (selfDestructTimer >= timeToSelfDestruct)
        {
            Explode(new Collision());
            return;
        }
    }
    public void OnCollision(Collision collision)
    {
        // Производится проверка на нахождения тега объекта в списках на взрыв и игнорирование
        bool explode = false;
        foreach (string tag in tagsToHit)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                explode = true;
                break;
            }
        }
        if (explode)
        {
            foreach (string tag2 in tagsToIgnore)
            {
                if (collision.gameObject.CompareTag(tag2))
                {
                    explode = false;
                    break;
                }
            }
        }
        if (explode)
        {
            Debug.Log($"Collided with {collision.gameObject.name}");
            isMoving = false;
            Explode(collision);
            return;
        }
    }
}
