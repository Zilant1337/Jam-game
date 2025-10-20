using UnityEngine;

public class WeaponRack : PaidInteractable
{
    [SerializeField]
    Transform weaponPrefabTransform;
    Weapon weapon;
    [SerializeField]
    Transform weaponPositionTransform;
    [SerializeField]
    float weaponRespawnTime;
    float weaponRespawnTimer;
    bool weaponSpawned;

    protected override void PaidAction()
    {
        LookAndShoot.instance.GetNewWeapon(weapon);
        weaponSpawned = false;
    }

    void Start()
    {
        weaponRespawnTimer = 0;
        SpawnWeapon();
        if (price == 0)
            price = weapon.Price;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponSpawned)
        {
            weaponRespawnTimer += Time.deltaTime;
            if (weaponRespawnTimer >= weaponRespawnTime)
            {
                weaponRespawnTimer = 0;
                SpawnWeapon();
            }
        }
    }
    void SpawnWeapon()
    {
        Transform weaponTransform = Instantiate(weaponPrefabTransform, weaponPositionTransform.position, weaponPositionTransform.rotation, this.transform);
        weapon = weaponTransform.GetComponent<Weapon>();
        weaponSpawned = true;
        paidInteractableUI.UpdatePaidInteractableText(weapon.WeaponName,weapon.Price.ToString());
    }
    public override string ToString()
    {
        return $"Buy {weapon}: {weapon.Price}";
    }
}
