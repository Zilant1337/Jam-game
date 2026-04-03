using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected bool activateOnTrigger;
    protected bool isInteractable = false;

    
    public bool ActivateOnTrigger { get => activateOnTrigger; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (activateOnTrigger)
        {
            InteractAction(other);
        }
        else
        {
            isInteractable = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        isInteractable = false;
    }
    public abstract void InteractAction(Collider other);
    public abstract string ButtonPromptText();
}
