using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
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
    private Vector2 previousDirection = Vector2.zero;
    void Start()
    {
        
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector2 movementDirection = CharacterScript.inputSystem.Player.Move.ReadValue<Vector2>();
        if (CharacterScript.inputSystem.Player.Look.ReadValue<Vector2>() == Vector2.zero && movementDirection != Vector2.zero)
        {
            transform.LookAt(new Vector3(transform.position.x + movementDirection.x, transform.position.y, transform.position.z + movementDirection.y));
        }
        if (isDodging)
        {
            transform.position += new Vector3(previousDirection.x, 0, previousDirection.y) * dodgeSpeed * Time.deltaTime;
            dodgedDistance += new Vector3(previousDirection.x, 0, previousDirection.y).magnitude * dodgeSpeed * Time.deltaTime;
            if (dodgedDistance >= dodgeDistance)
            {
                isDodging = false;
                dodgedDistance = 0;
            }
        }
        else
        {
            if (movementDirection != Vector2.zero)
                previousDirection = movementDirection;
            if (moveSpeed < MAX_MOVE_SPEED)
                moveSpeed += acceleration;
            transform.position += new Vector3(movementDirection.x, 0, movementDirection.y) * moveSpeed * Time.deltaTime;
        }
    }
    public void Dodge(InputAction.CallbackContext context)
    {
        if(context.performed)
        { 
            if (staminaScript.CurrentStamina >= dodgeStaminaCost)
            {
                staminaScript.RemoveStamina(dodgeStaminaCost);
                isDodging = true;
                previousDirection = previousDirection / previousDirection.magnitude;
            }
        }
    }
}
