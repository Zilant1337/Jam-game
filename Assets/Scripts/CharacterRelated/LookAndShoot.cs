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
    AmmoCounter ammoCounterScript;
    [SerializeField]
    private GameObject weaponsParent;
    [SerializeField]
    private Transform currentWeaponLocation;
    [SerializeField] 
    private Transform secondaryWeaponLocation;
    [SerializeField]
    protected Weapon currentWeapon;
    [SerializeField]
    protected Weapon secondaryWeapon;

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
        AmmoCounter.weaponChanged.Invoke(currentWeapon.AmmoCountInMag, currentWeapon.AmmoCount, currentWeapon.WeaponName, currentWeapon.PreviewImage);
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
            if (currentWeapon.IsAutomatic)
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
        currentWeapon.Shoot();
    }
    // Вызов функции перезарядки у основного оружия
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Reload();
        }
    }
    public void GetNewWeapon(Weapon newWeapon)
    {
        Debug.Log($"Getting {newWeapon.WeaponName}");
        if (currentWeapon.WeaponName == newWeapon.WeaponName)
        {
            Debug.Log($"Already got {newWeapon.WeaponName} as current weapon, refilling");
            currentWeapon.AmmoCount = currentWeapon.MAX_AMMO;
            currentWeapon.AmmoCountInMag = currentWeapon.MagSize;
            Debug.Log($"Refilled ammo in mag to {currentWeapon.AmmoCountInMag} and total ammo to {currentWeapon.AmmoCount}");
            AmmoCounter.ammoCountChanged.Invoke(currentWeapon.AmmoCountInMag,currentWeapon.AmmoCount);
            return;
        }
        
        if(secondaryWeapon!=null){
            if (secondaryWeapon.WeaponName == newWeapon.WeaponName)
            {
                Debug.Log($"Already got {newWeapon.WeaponName} as secondary weapon, refilling");
                secondaryWeapon.AmmoCount = secondaryWeapon.MAX_AMMO;
                secondaryWeapon.AmmoCountInMag = secondaryWeapon.MagSize;
                return;
            }
            Debug.Log($"Replacing current weapon with {newWeapon.WeaponName}");
            newWeapon.transform.parent = weaponsParent.transform;
            newWeapon.transform.position = currentWeaponLocation.position;
            newWeapon.transform.rotation = currentWeaponLocation.rotation;
            Destroy(currentWeapon.gameObject);
            currentWeapon = newWeapon;
            AmmoCounter.weaponChanged.Invoke(currentWeapon.AmmoCountInMag, currentWeapon.AmmoCount, currentWeapon.WeaponName, currentWeapon.PreviewImage);
        }
        else
        {
            Debug.Log($"Adding {newWeapon.WeaponName} as a secondary weapon");
            newWeapon.transform.parent = weaponsParent.transform;
            newWeapon.transform.position = secondaryWeaponLocation.position;
            newWeapon.transform.rotation = secondaryWeaponLocation.rotation;
            secondaryWeapon = newWeapon;
        }
    }
    public void SwitchWeapons(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentWeapon.ReloadTimer == 0 && secondaryWeapon!=null) 
            {
                // Меняем основное и дополнительное оружия местами
                currentWeapon.transform.position = secondaryWeaponLocation.position;
                currentWeapon.transform.rotation = secondaryWeaponLocation.rotation;
                secondaryWeapon.transform.position = currentWeaponLocation.position;
                secondaryWeapon.transform.rotation = currentWeaponLocation.rotation;
                // Переназначаем основное и запасное оружие в коде
                Weapon temp = currentWeapon;
                currentWeapon = secondaryWeapon;
                secondaryWeapon = temp;
                // Вызываем событие, меняющее элемент интерфейса
                AmmoCounter.weaponChanged.Invoke(currentWeapon.AmmoCountInMag, currentWeapon.AmmoCount, currentWeapon.WeaponName, currentWeapon.PreviewImage);
            }
            
        }
    }
}
