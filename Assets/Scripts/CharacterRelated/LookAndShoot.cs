using UnityEngine;
using UnityEngine.InputSystem;

public class LookAndShoot : MonoBehaviour
{
    public static LookAndShoot instance;

    [SerializeField]
    private float cursorDistance;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private GameObject gunsParent;
    [SerializeField]
    private Transform currentGunLocation;
    [SerializeField] 
    private Transform secondaryGunLocation;
    [SerializeField]
    protected Gun currentGun;
    [SerializeField]
    protected Gun secondaryGun;

    private bool keepShooting;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Can't have more than one LookAndShoot");
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
        keepShooting = false;
        if (currentGun.HasInfiniteAmmo)
            AmmoCounter.weaponChanged.Invoke("\u221E", "\u221E", currentGun.GunName, currentGun.PreviewImage);
        else
            AmmoCounter.weaponChanged.Invoke(currentGun.AmmoCountInMag.ToString(), currentGun.AmmoCount.ToString(), currentGun.GunName, currentGun.PreviewImage);
    }
    void Update()
    {
        Vector3 aimPoint;
        Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>();

        if (CursorManager.KeyboardActive)
        {
            // Альтернативный расчёт направления взгляда когда схема управления - клавиатура и мышь
            lookDirection = new Vector2(CursorManager.instance.MiddleOfCanvasTransform.position.x, CursorManager.instance.MiddleOfCanvasTransform.position.y)-lookDirection;
            lookDirection = -1*lookDirection.normalized;   
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
        aimPoint = new Vector3(transform.position.x + lookDirection.x * cursorDistance, transform.position.y, transform.position.z + lookDirection.y * cursorDistance);
        if (lookDirection != Vector2.zero)
        {            
            // Поворот персонажа в направлении курсора
            transform.LookAt(aimPoint);
            if (CursorManager.TouchActive)
            {
                Fire();
            }
        }
        if (keepShooting)
        {
            Fire();
        }
    }
    // Вызов функции выстрела у основного оружия
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();
            // Если оружие предполагается автоматическим, продолжаем стрелять пока игрок не отпустит кнопку стрельбы
            if (currentGun.IsAutomatic)
            {
                keepShooting = true;
            }
        }
        if (context.canceled)
        {
            keepShooting=false;
        }
    }
    private void Fire()
    {
        currentGun.Shoot();
        if (!currentGun.HasInfiniteAmmo)
            AmmoCounter.ammoCountChanged.Invoke(currentGun.AmmoCountInMag, currentGun.AmmoCount);
    }
    // Вызов функции перезарядки у основного оружия
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentGun.Reload();
        }
    }
    public void GetNewWeapon(Gun newWeapon)
    {
        Debug.Log($"Getting {newWeapon.GunName}");
        if (currentGun.GunName == newWeapon.GunName && !currentGun.HasInfiniteAmmo)
        {
            Debug.Log($"Already got {newWeapon.GunName} as current weapon, refilling");
            currentGun.AmmoCount = currentGun.MAX_AMMO;
            currentGun.AmmoCountInMag = currentGun.MagSize;
            Debug.Log($"Refilled ammo in mag to {currentGun.AmmoCountInMag} and total ammo to {currentGun.AmmoCount}");
            AmmoCounter.ammoCountChanged.Invoke(currentGun.AmmoCountInMag,currentGun.AmmoCount);
            return;
        }
        
        if(secondaryGun!=null){
            if (secondaryGun.GunName == newWeapon.GunName && !secondaryGun.HasInfiniteAmmo)
            {
                Debug.Log($"Already got {newWeapon.GunName} as secondary weapon, refilling");
                secondaryGun.AmmoCount = secondaryGun.MAX_AMMO;
                secondaryGun.AmmoCountInMag = secondaryGun.MagSize;
                return;
            }
            Debug.Log($"Replacing current weapon with {newWeapon.GunName}");
            newWeapon.transform.parent = gunsParent.transform;
            newWeapon.transform.position = currentGunLocation.position;
            newWeapon.transform.rotation = currentGunLocation.rotation;
            Destroy(currentGun.gameObject);
            currentGun = newWeapon;
            if (currentGun.HasInfiniteAmmo)
                AmmoCounter.weaponChanged.Invoke("\u221E", "\u221E", currentGun.GunName, currentGun.PreviewImage);
            else
                AmmoCounter.weaponChanged.Invoke(currentGun.AmmoCountInMag.ToString(), currentGun.AmmoCount.ToString(), currentGun.GunName, currentGun.PreviewImage);
        }
        else
        {
            Debug.Log($"Adding {newWeapon.GunName} as a secondary weapon");
            newWeapon.transform.parent = gunsParent.transform;
            newWeapon.transform.position = secondaryGunLocation.position;
            newWeapon.transform.rotation = secondaryGunLocation.rotation;
            secondaryGun = newWeapon;
        }
    }
    public void AddAmmo(float ammoAmount)
    {
        // Если у нас есть что пополнять в оружии в руках, пополняем
        if (currentGun.AmmoCount != currentGun.MAX_AMMO)
        {
            currentGun.AmmoCount += (int)(currentGun.MAX_AMMO * ammoAmount);
            AmmoCounter.ammoCountChanged.Invoke(currentGun.AmmoCountInMag, currentGun.AmmoCount);
        }
        // Если нет, пополняем оружие на спине
        else
        {
            secondaryGun.AmmoCount += (int)(secondaryGun.MAX_AMMO * ammoAmount);
        }
    }
    public void SwitchWeapons(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentGun.ReloadTimer == 0 && secondaryGun!=null) 
            {
                // Меняем основное и дополнительное оружия местами
                currentGun.transform.position = secondaryGunLocation.position;
                currentGun.transform.rotation = secondaryGunLocation.rotation;
                secondaryGun.transform.position = currentGunLocation.position;
                secondaryGun.transform.rotation = currentGunLocation.rotation;
                // Переназначаем основное и запасное оружие в коде
                Gun temp = currentGun;
                currentGun = secondaryGun;
                secondaryGun = temp;
                // Вызываем событие, меняющее элемент интерфейса
                if (currentGun.HasInfiniteAmmo)
                    AmmoCounter.weaponChanged.Invoke("\u221E", "\u221E", currentGun.GunName, currentGun.PreviewImage);
                else
                    AmmoCounter.weaponChanged.Invoke(currentGun.AmmoCountInMag.ToString(), currentGun.AmmoCount.ToString(), currentGun.GunName, currentGun.PreviewImage);
            }
            
        }
    }
}
