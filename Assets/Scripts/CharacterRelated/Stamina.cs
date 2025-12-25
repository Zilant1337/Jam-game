using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private float MAX_STAMINA;
    [SerializeField]
    protected float staminaRegenRate;
    private float currentStamina = 0;
    protected bool regenStamina;

    [SerializeField]
    protected ProgressBar staminaBarScript;

    public UnityEvent regenPause;
    public UnityEvent regenResume;
    public UnityEvent<float> removeStamina;

    public float CurrentStamina { get => currentStamina;}
    public bool IsStaminaFull => currentStamina == MAX_STAMINA?true:false;

    public float MaxStamina { get => MAX_STAMINA;}

    void Start()
    {
        regenPause= new UnityEvent();
        regenResume= new UnityEvent();
        removeStamina = new UnityEvent<float>();

        regenPause.AddListener(StopStaminaRegen);
        regenResume.AddListener(StartStaminaRegen);
        removeStamina.AddListener(RemoveStamina);

        currentStamina = MAX_STAMINA;
        regenStamina = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < MAX_STAMINA && regenStamina)
        {
            AddStamina(Time.deltaTime*staminaRegenRate);
        }
        staminaBarScript.UpdateProgressBar(currentStamina / MAX_STAMINA);
    }
    private void RemoveStamina(float staminaToRemove)
    {
        Debug.Log($"Removed {staminaToRemove} stamina!");
        currentStamina -= staminaToRemove;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }
    private void AddStamina(float staminaToAdd)
    {
        currentStamina += staminaToAdd;
        if (currentStamina >MAX_STAMINA)
        {
            currentStamina = MAX_STAMINA;
        }
    }
    private void StartStaminaRegen()
    {
        regenStamina = true;
    }
    private void StopStaminaRegen()
    {
        regenStamina = false;
    }
    public void ResetStamina()
    {
        currentStamina = 0;
    }
}
