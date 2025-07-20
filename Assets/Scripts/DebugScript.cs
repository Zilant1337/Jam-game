using UnityEngine;
using UnityEngine.InputSystem;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    Transform Cube;
    private void Update()
    {
        Vector3 MousePosition = Mouse.current.position.ReadValue();
        MousePosition.z = Camera.main.nearClipPlane;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        Cube.position = new Vector3(MousePosition.x, transform.position.y, MousePosition.z); 
    }
}
