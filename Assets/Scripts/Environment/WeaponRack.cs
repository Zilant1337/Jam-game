using UnityEngine;

public class WeaponRack : Interactable
{
    [SerializeField]
    Weapon weapon;
    [SerializeField]
    WeaponRackUI weaponRackUI;
    [SerializeField]
    Transform weaponPositionTransform;

    public override void InteractAction()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        weapon.transform.position = weaponPositionTransform.position;
        weapon.transform.rotation = weaponPositionTransform.rotation;
        weaponRackUI.UpdateWeaponRackText(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
