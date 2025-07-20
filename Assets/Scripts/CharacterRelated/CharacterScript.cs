using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    protected Health healthScript;
    [SerializeField]
    protected Movement movementScript;
    [SerializeField]
    protected Melee meleeScript;
    [SerializeField]
    protected LookAndShoot lookAndShootScript;

    public static InputSystem_Actions inputSystem;
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
