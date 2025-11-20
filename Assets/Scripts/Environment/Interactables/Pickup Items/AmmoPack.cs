using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Interactable
{
    [SerializeField, Range(0f, 1f)]
    // as a decimal 0-1
    float ammoRestored;
    [SerializeField]
    List<string> tagsToGiveAmmoTo;
    bool canGiveAmmo;
    public override void InteractAction(Collider other)
    {
        if (canGiveAmmo)
        {
            bool giveAmmo = false;
            foreach (string tag in tagsToGiveAmmoTo)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    giveAmmo = true;
                    break;
                }
            }
            if (giveAmmo)
            {
                LookAndShoot guns = other.GetComponent<LookAndShoot>();
                if (guns != null)
                {
                    guns.AddAmmo(ammoRestored);
                    canGiveAmmo = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canGiveAmmo = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
