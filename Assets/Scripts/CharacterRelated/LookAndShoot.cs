using UnityEngine;
using UnityEngine.InputSystem;

public class LookAndShoot : MonoBehaviour
{
    [SerializeField]
    private float cursorDistance;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private GameObject weaponsParent;
    [SerializeField]
    protected Weapon currentWeapon;
    [SerializeField]
    protected Weapon secondaryWeapon;
    void Start()
    {
        
    }
    void Update()
    {
        /*Vector3 MousePosition = Mouse.current.position.ReadValue();
        MousePosition.z = Camera.main.nearClipPlane;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        transform.LookAt(new Vector3(MousePosition.x, transform.position.y, MousePosition.z));*/
        Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
        if (lookDirection != Vector2.zero)
        {
            Cursor.visible = true ;
            Vector3 aimPoint = new Vector3(transform.position.x + lookDirection.x * cursorDistance, transform.position.y, transform.position.z + lookDirection.y * cursorDistance);
            Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(aimPoint));
            transform.LookAt(aimPoint);
        }
        if(lookDirection == Vector2.zero)
        {
            Cursor.visible = false;
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Shoot();
        }
    }
}
