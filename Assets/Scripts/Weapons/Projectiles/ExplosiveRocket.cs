using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRocket : Rocket
{
    [SerializeField]
    protected bool dealDamageOnImpact;
    protected List<Health> healthsToDamage;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && !healthsToDamage.Contains(health))
        {
            healthsToDamage.Add(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            healthsToDamage.Remove(health);
        }
        
    }
    protected override List<Health> GetHealthsToDamage(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && dealDamageOnImpact)
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
