using UnityEngine;

public class WeaponRack : PaidInteractable
{
    [SerializeField]
    Transform weaponPrefabTransform;
    Gun weapon;
    [SerializeField]
    Transform weaponPositionTransform;
    [SerializeField]
    float weaponRespawnTime;
    float weaponRespawnTimer;
    bool weaponSpawned;

    protected override bool PaidAction()
    {
        weaponSpawned = false;
        return LookAndShoot.instance.GetNewWeapon(weapon);
    }

    void Start()
    {
        weaponRespawnTimer = 0;
        SpawnWeapon();
        if (price == 0)
            price = weapon.Price;
    }
    protected override bool CheckReadiness()
    {
        return weaponSpawned;
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
        weapon = weaponTransform.GetComponent<Gun>();
        weaponSpawned = true;
        paidInteractableUI.UpdatePaidInteractableText(weapon.GunName,weapon.Price.ToString());
    }
    public override string ToString()
    {
        return $"Buy {weapon}: {weapon.Price}";
    }
}
