using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    Texture2D cursorSprite;
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursorSprite.width / 2, cursorSprite.width / 2);
        Cursor.SetCursor(cursorSprite,cursorHotspot,CursorMode.Auto);
    }
}
