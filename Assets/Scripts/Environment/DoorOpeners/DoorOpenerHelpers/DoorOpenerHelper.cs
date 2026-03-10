using UnityEngine;

public abstract class DoorOpenerHelper : Interactable
{ 
    [SerializeField]
    protected ConditionalDoorOpener doorOpener;
    protected bool isActivated;

    public bool IsActivated { get => isActivated; set => isActivated = value; }

    virtual protected void Start()
    {
        isActivated = false;
    }

    public void ReplaceDoorOpener(ConditionalDoorOpener doorOpener)
    {
        this.doorOpener = doorOpener;
    }
    public void ProgressDoorOpener()
    {
        doorOpener.ProgressOpening(this, true);
    }
}
