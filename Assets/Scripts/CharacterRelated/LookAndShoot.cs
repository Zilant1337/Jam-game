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
    protected Weapon currentWeapon;
    [SerializeField]
    protected Weapon secondaryWeapon;
    void Start()
    {
        ammoCounterScript.UpdateAmmoCounterFull(currentWeapon.AmmoCountInMag,currentWeapon.AmmoCount,currentWeapon.WeaponName,currentWeapon.PreviewImage);
    }
    void Update()
    {
        /*Vector3 MousePosition = Mouse.current.position.ReadValue();
        MousePosition.z = Camera.main.nearClipPlane;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        transform.LookAt(new Vector3(MousePosition.x, transform.position.y, MousePosition.z));*/

        Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
        // Расчитывается направление взгляда, персонаж к нему поворачивается и курсор в виде прицела перемещается по направлению взгляда
        if (lookDirection != Vector2.zero)
        {
            Cursor.visible = true ;
            Vector3 aimPoint = new Vector3(transform.position.x + lookDirection.x * cursorDistance, transform.position.y, transform.position.z + lookDirection.y * cursorDistance);
            Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(aimPoint));
            transform.LookAt(aimPoint);
        }
        // Если игрок никуда не смотрит, курсор исчезает
        if(lookDirection == Vector2.zero)
        {
            Cursor.visible = false;
        }
    }
    // У оружия вызывается функция выстрела
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Shoot();
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Reload();
        }
    }
}
