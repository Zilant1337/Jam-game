using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    private Health healthScript;
    [SerializeField]
    private Movement movementScript;
    [SerializeField]
    private LookAndShoot lookAndShootScript;
    [SerializeField]
    private MoneyAndPurchasing moneyAndPurchasing;
    [SerializeField]
    private Interact interact;

    public static InputSystem_Actions inputSystem;

    protected Health HealthScript { get => healthScript; }
    protected Movement MovementScript { get => movementScript; }
    protected LookAndShoot LookAndShootScript { get => lookAndShootScript; }
    protected MoneyAndPurchasing MoneyAndPurchasing { get => moneyAndPurchasing; }
    protected Interact Interact { get => interact; }

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
