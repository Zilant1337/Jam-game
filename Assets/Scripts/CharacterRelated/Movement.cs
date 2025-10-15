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

    protected float gravity;
    protected float verticalSpeed;
    protected bool isDodging=false;
    protected float dodgedDistance = 0;
    protected Vector2 previousDirection = Vector2.up;
    protected Vector3 previousPosition = Vector3.zero;
    void Start()
    {
        verticalSpeed = 0;
        gravity = this.GetComponent<Rigidbody>().mass;
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

        if (characterController.isGrounded)
        {
            verticalSpeed = 0;
        }
        verticalSpeed -= gravity * Time.deltaTime;
        Vector3 moveVector = new Vector3(0,verticalSpeed,0);
        // Если игрок не смотрит отдельно в какую то сторону и перемещается, персонаж поворачивается по направлению движения
        if (CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>() == Vector2.zero && movementDirection != Vector2.zero)
        {
            transform.LookAt(new Vector3(transform.position.x + movementDirection.x, transform.position.y, transform.position.z + movementDirection.y));
        }
        
        // Если игрок нажал на кнопку рывка, то он перемещается в одно направление пока не закончится рывок
        if (isDodging)
        {
            // Перемещение с помощью Character Controller
            moveVector.x = previousDirection.x * dodgeSpeed;
            moveVector.z = previousDirection.y * dodgeSpeed;
        }
        
        // Ускоряем персонажа и начинаем перемещать его в направлении движения
        else
        {
            if (movementDirection != Vector2.zero)
            {
                previousDirection = movementDirection;
                if (moveSpeed < MAX_MOVE_SPEED)
                    moveSpeed += acceleration;
            }
            else
            {
                if (moveSpeed > 0)
                {
                    moveSpeed-=acceleration;
                    if (moveSpeed < 0)
                    {
                        moveSpeed = 0;
                    }
                }
            }
                // Перемещение с помощью Character Controller
                moveVector.x = movementDirection.x * moveSpeed;
            moveVector.z = movementDirection.y * moveSpeed;
        }
        characterController.Move(moveVector * Time.deltaTime);

        if (isDodging)
        {
            dodgedDistance += Vector3.Distance(this.transform.position, previousPosition);
            if (dodgedDistance >= dodgeDistance)
            {
                isDodging = false;
                dodgedDistance = 0;
                staminaScript.regenResume.Invoke();
            }
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
