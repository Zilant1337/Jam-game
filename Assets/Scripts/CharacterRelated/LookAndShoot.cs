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
        /*Vector3 MousePosition = Mouse.current.position.ReadValue();
        MousePosition.z = Camera.main.nearClipPlane;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        transform.LookAt(new Vector3(MousePosition.x, transform.position.y, MousePosition.z));*/

        Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
        // Если ввод направления есть, повернуть персонажа в ту сторону, куда игрок вводит
        if (lookDirection != Vector2.zero)
        {
            Cursor.visible = true ;
            Vector3 aimPoint = new Vector3(transform.position.x + lookDirection.x * cursorDistance, transform.position.y, transform.position.z + lookDirection.y * cursorDistance);
            // Перемещение курсора на определённое расстояние от персонажа
            Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(aimPoint));
            transform.LookAt(aimPoint);
        }
        // Отключение курсора при отсутствии ввода для взгляда
        if(lookDirection == Vector2.zero)
        {
            Cursor.visible = false;
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
            // Меняем основное и дополнительное оружия местами
            currentWeapon.transform.position = secondaryWeaponLocation.position;
            currentWeapon.transform.rotation = secondaryWeaponLocation.rotation;
            secondaryWeapon.transform.position = currentWeaponLocation.position;
            secondaryWeapon.transform.rotation= currentWeaponLocation.rotation;
            // Переназначаем основное и запасное оружие в коде
            Weapon temp = currentWeapon;
            currentWeapon = secondaryWeapon;
            secondaryWeapon = temp;
            // Вызываем событие, меняющее элемент интерфейса
            AmmoCounter.weaponChanged.Invoke(currentWeapon.AmmoCountInMag, currentWeapon.AmmoCount, currentWeapon.WeaponName, currentWeapon.PreviewImage);
        }
    }
}
