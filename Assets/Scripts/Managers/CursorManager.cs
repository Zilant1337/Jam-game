using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

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

    static bool keyboardActive;

    public static bool KeyboardActive { get => keyboardActive; }
    public Transform MiddleOfCanvasTransform { get => middleOfCanvasTransform; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Can't have more than one cursor manager");
            Destroy(gameObject);
        }
        virtualCursorTransform.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!keyboardActive)
        {
            Gamepad gamepad = Gamepad.current;
            Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
            if (lookDirection != Vector2.zero)
            {
                if(virtualCursorTransform.gameObject.activeSelf==false)
                    virtualCursorTransform.gameObject.SetActive(true);
                Vector2 aimPoint = new Vector2(middleOfCanvasTransform.position.x + lookDirection.x * cursorDistance, middleOfCanvasTransform.position.y+lookDirection.y*cursorDistance);
                
                virtualCursorTransform.position = aimPoint;
            }
            // Отключение курсора при отсутствии ввода для взгляда
            if (lookDirection == Vector2.zero)
            {
                virtualCursorTransform.gameObject.SetActive(false);
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
            Cursor.visible = true;
            virtualCursorTransform.gameObject.SetActive(false);
            keyboardActive = true;
        }
        else
        {    
            Cursor.visible = false;
            virtualCursorTransform.gameObject.SetActive(true);
            keyboardActive = false;
            CharacterScript.inputSystem.Player.Look.Reset();
        }
        Debug.Log($"Keyboard active: {keyboardActive}");
    } 
}
