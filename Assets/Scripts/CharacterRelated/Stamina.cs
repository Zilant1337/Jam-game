using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private float MAX_STAMINA;
    [SerializeField]
    protected float staminaRegenRate;
    private float currentStamina = 0;

    [SerializeField]
    protected StaminaBar staminaBarScript;

    public float CurrentStamina { get => currentStamina;}
    public bool IsStaminaFull => currentStamina == MAX_STAMINA?true:false;

    public float MaxStamina { get => MAX_STAMINA;}

    void Start()
    {
        currentStamina = MAX_STAMINA;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < MAX_STAMINA)
        {
            AddStamina(Time.deltaTime*staminaRegenRate);
        }
        staminaBarScript.UpdateStaminaBar(currentStamina / MAX_STAMINA);
    }
    public void RemoveStamina(float staminaToRemove)
    {
        Debug.Log($"Removed {staminaToRemove} stamina!");
        currentStamina -= staminaToRemove;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }
    public void AddStamina(float staminaToAdd)
    {
        currentStamina += staminaToAdd;
        if (currentStamina >MAX_STAMINA)
        {
            currentStamina = MAX_STAMINA;
        }
    }
    public void ResetStamina()
    {
        currentStamina = 0;
    }
}
