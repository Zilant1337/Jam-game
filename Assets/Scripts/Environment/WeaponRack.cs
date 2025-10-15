using UnityEngine;

public class WeaponRack : Interactable
{
    [SerializeField]
    Transform weaponPrefabTransform;
    Weapon weapon;
    [SerializeField]
    WeaponRackUI weaponRackUI;
    [SerializeField]
    Transform weaponPositionTransform;
    [SerializeField]
    float weaponRespawnTime;
    float weaponRespawnTimer;
    bool weaponSpawned;
    public override void InteractAction()
    {
        if (MoneyAndPurchasing.instance.RemoveMoney(weapon.Price))
        {
            Debug.Log($"Removed {weapon.Price}");
            LookAndShoot.instance.GetNewWeapon(weapon);
            weaponSpawned = false;
        }
        else
        {
            Debug.Log($"Not enough money!");
        }
    }

    void Start()
    {
        weaponRespawnTimer = 0;
        SpawnWeapon();
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
        weaponRackUI.UpdateWeaponRackText(weapon);
    }
    public override string ToString()
    {
        return $"Buy {weapon}: {weapon.Price}";
    }
}
