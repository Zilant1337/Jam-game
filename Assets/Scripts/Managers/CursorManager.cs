using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Texture2D cursorSprite;
    Mouse realMouse;
    Mouse virtualMouse;
    private void Awake()
    {
        realMouse = Mouse.current;
        virtualMouse = new Mouse();
    }
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursorSprite.width / 2, cursorSprite.width / 2);
        Cursor.SetCursor(cursorSprite,cursorHotspot,CursorMode.Auto);
    }
}
