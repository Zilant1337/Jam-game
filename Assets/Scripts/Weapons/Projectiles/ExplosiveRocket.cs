using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRocket : Rocket
{
    protected List<Health> healthsToDamage;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
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
        return healthsToDamage;
    }
    void Start()
    {
        selfDestructTimer = 0;
        isAccellerating = true;
        canDamage = true;
        healthsToDamage = new List<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
