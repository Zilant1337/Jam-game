using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

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
        Debug.Log($"Entered {other.gameObject.name}'s trigger");
        Interactable touchedInteractable = other.GetComponent<Interactable>();
        if (touchedInteractable != null)
        {
            if (touchedInteractable.ActivateOnTrigger)
                touchedInteractable.InteractAction();
            else if(interactable ==null || Vector3.Distance(this.transform.position,touchedInteractable.transform.position)< Vector3.Distance(this.transform.position, interactable.transform.position))
                interactable = touchedInteractable;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Left {other.gameObject.name}'s trigger");
        Interactable touchedInteractable = other.GetComponent<Interactable>();
        if (touchedInteractable != null)
        {
            if(interactable == touchedInteractable)
            {
                interactable = null;
            }
        }
    }
    public void ActivateInteractable(InputAction.CallbackContext context)
    {
        if (context.performed && interactable != null)
        {
            Debug.Log($"Activating interactable {interactable.name}!");
            interactable.InteractAction();
        }
    }
    void Update()
    {
        
    }
}
