using UnityEngine;
using UnityEngine.InputSystem;


public class Interact : MonoBehaviour
{
    public static Interact instance;
    [SerializeField]
    CharacterScript character;
    Interactable interactable;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Can't have more than one Interact");
            Destroy(this);
            return;
        }
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactable touchedInteractable = other.GetComponent<Interactable>();
        if (touchedInteractable != null)
        {
            if (!touchedInteractable.ActivateOnTrigger || interactable == null || Vector3.Distance(this.transform.position, touchedInteractable.transform.position) < Vector3.Distance(this.transform.position, interactable.transform.position))
            {
                interactable = touchedInteractable;
                string buttonPromptText = interactable.ButtonPromptText();
                if(buttonPromptText != "")
                {
                    if(CursorManager.TouchActive)
                        UniversalButtonPrompt.onStart.Invoke($"Interact to {buttonPromptText}");
                    if (CursorManager.KeyboardActive)
                        UniversalButtonPrompt.onStart.Invoke($"{CharacterScript.inputSystem.FindAction("Interact").GetBindingDisplayString(group: "Keyboard&Mouse")} to {buttonPromptText}");
                    if(!CursorManager.TouchActive && !CursorManager.KeyboardActive)
                        UniversalButtonPrompt.onStart.Invoke($"{CharacterScript.inputSystem.FindAction("Interact").GetBindingDisplayString(group: "Gamepad")} to {buttonPromptText}");
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Interactable touchedInteractable = other.GetComponent<Interactable>();
        if (touchedInteractable != null)
        {
            if(interactable == touchedInteractable)
            {
                UniversalButtonPrompt.onFinish.Invoke();
                interactable = null;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        string buttonPromptText = interactable.ButtonPromptText();
        if (buttonPromptText=="")
        {
            UniversalButtonPrompt.onFinish.Invoke();
        }
    }
    public void ActivateInteractable(InputAction.CallbackContext context)
    {
        if (context.performed && interactable != null)
        {
            Debug.Log($"Activating interactable {interactable.name}!");
            interactable.InteractAction(this.GetComponent<Collider>());
        }
    }
    void Update()
    {
        
    }
}
