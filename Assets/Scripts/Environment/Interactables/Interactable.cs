using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum Interactions 
    {
       Buy,
       Activate
    }
    [SerializeField]
    protected bool activateOnTrigger;
    protected bool isInteractable = false;
    [SerializeField]
    protected Interactions interaction;

    public bool ActivateOnTrigger { get => activateOnTrigger; }

    public void OnTriggerEnter(Collider other)
    {
        if (activateOnTrigger)
        {
            InteractAction();
        }
        else
        {
            isInteractable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
    }
    public abstract void InteractAction();
}
