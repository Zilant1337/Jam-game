using UnityEngine;

public class EnemyWeaponManager: MonoBehaviour
{
    [SerializeField]
    private GameObject gunsParent;
    [SerializeField]
    protected Gun currentGun;

    void Update()
    {

    }
    // Вызов функции выстрела у основного оружия
    public bool Shoot()
    {
        return currentGun.Shoot();
    }

    // Вызов функции перезарядки у основного оружия
    public void Reload()
    {
       currentGun.Reload();
    }
}
