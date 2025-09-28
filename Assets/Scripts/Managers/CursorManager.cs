using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private float cursorDistance;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Texture2D cursorSprite;
    [SerializeField]
    Transform middleOfCanvasTransform;
    [SerializeField]
    Transform virtualCursorTransform;
    Mouse realMouse;
    bool keyboardActive;

    private void Awake()
    {
        
        realMouse = Mouse.current;
        virtualCursorTransform.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!keyboardActive)
        {
            Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
            if (lookDirection != Vector2.zero)
            {
                virtualCursorTransform.gameObject.SetActive(true);
                Vector3 aimPoint = new Vector2(middleOfCanvasTransform.position.x + lookDirection.x * cursorDistance, middleOfCanvasTransform.position.y+lookDirection.y*cursorDistance);
                virtualCursorTransform.position = aimPoint;
            }
            // Отключение курсора при отсутствии ввода для взгляда
            if (lookDirection == Vector2.zero)
            {
                virtualCursorTransform.gameObject.SetActive(false);
                Cursor.visible = false;
            }
        }
        
    }
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursorSprite.width / 2, cursorSprite.width / 2);
        Cursor.SetCursor(cursorSprite,cursorHotspot,CursorMode.Auto);
        OnControlSchemeChange();
    }
    public void OnControlSchemeChange()
    {
        if(playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            keyboardActive = true;
            Cursor.visible = true;
            virtualCursorTransform.gameObject.SetActive(false);
        }
        else
        {
            keyboardActive = false;  
            Cursor.visible = false;
            virtualCursorTransform.gameObject.SetActive(true);
        }
    } 
}
