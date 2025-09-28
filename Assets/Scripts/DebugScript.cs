using UnityEngine;
using UnityEngine.InputSystem;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    Transform Cube;
    private void Update()
    {
        Vector2 MousePosition = CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>();
        MousePosition = new Vector2(CursorManager.instance.MiddleOfCanvasTransform.position.x, CursorManager.instance.MiddleOfCanvasTransform.position.y) - MousePosition;
        MousePosition = MousePosition.normalized;
        Debug.Log(MousePosition);
    }
}
