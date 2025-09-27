using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    
    [SerializeField]
    private float cursorDistance;
    [SerializeField]
    private RectTransform canvasTransform; 
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private RectTransform cursorTransform;
    private Mouse virtualMouse;
    private void OnEnable()
    {
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position,position);
        }

        InputSystem.onAfterUpdate+=UpdateMotion;
    }
    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }
    private void UpdateMotion()
    {
        if(virtualMouse==null||Gamepad.current == null)
        {
            return;
        }
        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        Vector3 aimPoint = new Vector3(transform.position.x + stickValue.x * cursorDistance, transform.position.y, transform.position.z + stickValue.y * cursorDistance);
        InputState.Change(virtualMouse.position,aimPoint);
        AnchorPosition(aimPoint);
    }
    private void AnchorPosition(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform,position,Camera.main,out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
