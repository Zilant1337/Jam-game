using UnityEngine;
using UnityEngine.InputSystem;

public class LookAndShoot : MonoBehaviour
{
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
    public void SwitchWeapons(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentWeapon.ReloadTimer == 0) 
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
