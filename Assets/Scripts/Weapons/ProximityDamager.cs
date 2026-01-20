using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProximityDamager : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected List<string> tagsToHit;
    protected List<Health> healthsToDamage;

    protected void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && CheckTags(health.gameObject))
        {
            healthsToDamage.Add(health);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            if (healthsToDamage.Contains(health))
            {
                Debug.Log($"{health.gameObject.name} exited the proximity damage range");
                healthsToDamage.Remove(health);
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            if (healthsToDamage.Contains(health))
            {
                health.TakeDamage(damage);
            }
        }
        
    }
    protected bool CheckTags(GameObject gameObject)
    {
        foreach (string tag in tagsToHit)
        {
            if (gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthsToDamage = new List<Health>();
    }
}
