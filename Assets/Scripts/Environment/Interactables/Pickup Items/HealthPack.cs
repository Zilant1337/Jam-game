using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPack : Interactable
{
    [SerializeField]
    float healthHealed;
    [SerializeField]
    List<string> tagsToHeal;
    bool canHeal;
    public override void InteractAction(Collider other)
    {
        if (canHeal)
        {
            bool heal = false;
            foreach (string tag in tagsToHeal)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    heal = true;
                    break;
                }
            }
            if (heal)
            {
                Health health = other.GetComponent<Health>();
                if (health != null)
                {
                    health.AddHealth(healthHealed);
                    canHeal = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canHeal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
