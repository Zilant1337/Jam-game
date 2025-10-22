using UnityEngine;
using System.Collections.Generic;
public class Rocket : Projectile
{
    [SerializeField]
    protected float projectileSpeed;
    protected float currentSpeed;
    [SerializeField]
    protected float acceleration;
    [SerializeField]
    protected ParticleSystem rocketParticleSystem;
    [SerializeField] 
    protected ParticleSystem rocketExplosionParticleSystem;
    [SerializeField]
    protected Rigidbody rocketRigidbody;
    [SerializeField]
    protected float damage;
    bool isAccellerating;

    protected override void Move()
    {
        if (isAccellerating)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        if (currentSpeed > projectileSpeed)
        {
            currentSpeed = projectileSpeed;
            isAccellerating = false;
        }
        rocketRigidbody.AddForce(Vector3.forward*currentSpeed);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        Explode(collision);
    }
    protected virtual void Explode(Collision collision)
    {
        rocketExplosionParticleSystem.Play();
        rocketExplosionParticleSystem.transform.SetParent(null);
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        DealDamage(new List<Health> { enemyHealth });
        Destroy(this.gameObject);
    }
    protected virtual void DealDamage(List<Health> healthList)
    {
        foreach (Health health in healthList)
        {
            if (health != null)
                health.TakeDamage(damage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAccellerating = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
