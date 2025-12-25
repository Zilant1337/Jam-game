using UnityEngine;

public abstract class DoorOpenerHelper : Interactable
{
    [SerializeField]
    ConditionalDoorOpener doorOpener;
    public void ProgressDoorOpener()
    {
        doorOpener.ProgressOpening(this);
    }
}
