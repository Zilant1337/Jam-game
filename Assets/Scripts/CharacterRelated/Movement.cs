using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody characterRigidBody;
    [SerializeField]
    protected CharacterController characterController;
    [SerializeField]
    protected float MAX_MOVE_SPEED;
    protected float moveSpeed = 0;
    [SerializeField]
    protected float acceleration;
    
    [SerializeField] 
    protected float dodgeSpeed;
    [SerializeField]
    protected float dodgeDistance;
    [SerializeField]
    protected float dodgeStaminaCost;

    [SerializeField]
    protected Stamina staminaScript;

    private bool isDodging=false;
    private float dodgedDistance = 0;
    private Vector2 previousDirection = Vector2.up;
    private Vector3 previousPosition = Vector3.zero;
    void Start()
    {
        
    }
    void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        
    }
    void Move()
    {
        // Используется инпут система Unity для получения направления движения
        Vector2 movementDirection = CharacterScript.inputSystem.Player.Move.ReadValue<Vector2>();
        // Если игрок не смотрит отдельно в какую то сторону и перемещается, персонаж поворачивается по направлению движения
        if (CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>() == Vector2.zero && movementDirection != Vector2.zero)
        {
            transform.LookAt(new Vector3(transform.position.x + movementDirection.x, transform.position.y, transform.position.z + movementDirection.y));
        }
        // Если игрок нажал на кнопку рывка, то он перемещается в одно направление пока не закончится рывок
        if (isDodging)
        {
            // Перемещение с помощью Character Controller
            characterController.Move(new Vector3(previousDirection.x, 0, previousDirection.y) * dodgeSpeed * Time.deltaTime);
            dodgedDistance += Vector3.Distance(this.transform.position, previousPosition);
            if (dodgedDistance >= dodgeDistance)
            {
                isDodging = false;
                dodgedDistance = 0;
                staminaScript.regenResume.Invoke();
            }
        }
        // Ускоряем персонажа и начинаем перемещать его в направлении движения
        else
        {
            if (movementDirection != Vector2.zero)
                previousDirection = movementDirection;
            if (moveSpeed < MAX_MOVE_SPEED)
                moveSpeed += acceleration;
            // Перемещение с помощью Character Controller
            characterController.Move(new Vector3(movementDirection.x,0,movementDirection.y)*moveSpeed * Time.deltaTime);
        }
        previousPosition = this.transform.position;
    }
    public void Dodge(InputAction.CallbackContext context)
    {
        // Запускаем рывок если игрок нажал на кнопку рывка, проверяем количество выносливости и, если её достаточно, запускаем рывок
        if(context.performed)
        { 
            if (staminaScript.CurrentStamina >= dodgeStaminaCost && !isDodging)
            {
                staminaScript.removeStamina.Invoke(dodgeStaminaCost);
                staminaScript.regenPause.Invoke();
                isDodging = true;
                previousDirection = previousDirection / previousDirection.magnitude;
            }
        }
    }
}
