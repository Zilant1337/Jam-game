using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRocket : Rocket
{
    [SerializeField]
    protected bool dealDamageOnImpact;
    protected List<Health> healthsToDamage;
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name} entered {this.name} collider/trigger");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            AddHealthToDamage(health);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.gameObject.name} exited {this.name} collider/trigger");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            RemoveHealthToDamage(health);
        }

    }
    protected override List<Health> GetHealthsToDamage(Collider collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && dealDamageOnImpact && ToDamage(health))
        {
            healthsToDamage.Add(health);
        }
        return healthsToDamage;
    }
    void Start()
    {
        selfDestructTimer = 0;
        canDamage = true;
        healthsToDamage = new List<Health>();
        isMoving = true;
    }
    public void AddHealthToDamage(Health health)
    {
        if (!healthsToDamage.Contains(health))
        {
            Debug.Log($"{health.gameObject.name} added to healths to damage!");
            healthsToDamage.Add(health);
        }
    }
    public void RemoveHealthToDamage(Health health)
    {
        if (healthsToDamage.Contains(health))
        {
            Debug.Log($"{health.gameObject.name} exited the explosion range");
            healthsToDamage.Remove(health);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
