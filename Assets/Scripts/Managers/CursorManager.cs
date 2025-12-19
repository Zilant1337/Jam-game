using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    [SerializeField]
    private GameObject mobileInput;
    [SerializeField]
    private float cursorDistance;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Texture2D gameCursorSprite;
    [SerializeField] 
    Texture2D menuCursorSprite;
    [SerializeField]
    Transform middleOfCanvasTransform;
    [SerializeField]
    Transform virtualCursorTransform;
    [SerializeField]
    float touchInputDisableTime;

    static bool keyboardActive;
    static bool touchActive;

    protected float touchInputTimer = 0;

    public static bool KeyboardActive { get => keyboardActive; }
    public static bool TouchActive { get => touchActive; }
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
        if(Application.platform == RuntimePlatform.Android)
        {
            Application.targetFrameRate = 120;
        }
        if (SceneManager.GetActiveScene().name != "MainMenu")
            virtualCursorTransform.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (touchInputTimer > 0)
            {
                touchInputTimer -= Time.deltaTime;
                if (touchInputTimer <= 0)
                {
                    touchActive = false;
                    mobileInput.SetActive(false);
                }
            }
            if (!keyboardActive)
            {
                Vector2 lookDirection = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>().normalized;
                if (lookDirection != Vector2.zero)
                {
                    if (virtualCursorTransform.gameObject.activeSelf == false)
                        virtualCursorTransform.gameObject.SetActive(true);
                    Vector2 aimPoint = new Vector2(middleOfCanvasTransform.position.x + lookDirection.x * cursorDistance, middleOfCanvasTransform.position.y + lookDirection.y * cursorDistance);

                    virtualCursorTransform.position = aimPoint;
                }
                // Отключение курсора при отсутствии ввода для взгляда
                if (lookDirection == Vector2.zero)
                {
                    virtualCursorTransform.gameObject.SetActive(false);
                }
            }
            // Включение экранного управления если был ввод на тачскрин
            if (Touchscreen.current != null && Touchscreen.current.wasUpdatedThisFrame)
            {
                touchInputTimer = touchInputDisableTime;
                touchActive = true;
                mobileInput.SetActive(true);
            }
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SetGameCursor();
            keyboardActive = true;
            if (Touchscreen.current != null && Touchscreen.current.wasUpdatedThisFrame)
            {
                touchActive = true;
                keyboardActive = false;
                mobileInput.SetActive(true);
            }
            OnControlSchemeChange();
            CharacterScript.inputSystem.Player.Look.Reset();
        }
        else
        {
            SetMenuCursor();
        }
    }
    public void SetGameCursor()
    {
        Vector2 cursorHotspot = new Vector2(gameCursorSprite.width / 2, gameCursorSprite.width / 2);
        Cursor.SetCursor(gameCursorSprite, cursorHotspot, CursorMode.Auto);
    }
    public void SetMenuCursor()
    {
        Cursor.SetCursor(menuCursorSprite,Vector2.zero,CursorMode.Auto);
    }
    public void OnControlSchemeChange()
    {
        if(playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            touchActive = false;
            mobileInput.SetActive(false);
            Cursor.visible = true;
            virtualCursorTransform.gameObject.SetActive(false);
            keyboardActive = true;
        }
        else
        {
            /*if (Gamepad.current != null)
            {
                touchActive = false;
                mobileInput.SetActive(false);
            }*/
            Cursor.visible = false;
            virtualCursorTransform.gameObject.SetActive(true);
            keyboardActive = false;
            CharacterScript.inputSystem.Player.Look.Reset();
        }
        Debug.Log($"Keyboard active: {keyboardActive}");
    } 
}
