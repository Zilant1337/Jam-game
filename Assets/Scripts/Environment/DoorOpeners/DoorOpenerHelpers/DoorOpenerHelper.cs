using UnityEngine;

public abstract class DoorOpenerHelper : Interactable
{
    [SerializeField]
    protected ConditionalDoorOpener doorOpener;
    public void ReplaceDoorOpener(ConditionalDoorOpener doorOpener)
    {
        this.doorOpener = doorOpener;
    }
    public void ProgressDoorOpener()
    {
        doorOpener.ProgressOpening(this);
    }
}
