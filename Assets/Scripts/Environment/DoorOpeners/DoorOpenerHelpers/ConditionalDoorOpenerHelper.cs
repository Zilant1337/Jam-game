using UnityEngine;

public abstract class ConditionalDoorOpenerHelper : Interactable
{
    [SerializeField]
    protected ConditionalDoorOpener doorOpener;
    protected bool isActivated;

    public bool IsActivated { get => isActivated; set => isActivated = value; }
    public ConditionalDoorOpener DoorOpener { get => doorOpener; set => SetDoorOpener(value); }


    protected void SetDoorOpener(ConditionalDoorOpener doorOpener)
    {
        if (this.doorOpener == null)
        {
            this.doorOpener = doorOpener;
        }
        else
        {
            Debug.LogError($"Can't add another door opener to {this.name}");
        }
        return;
    }
    protected virtual void Awake()
    {
        isActivated = false;
    }
    virtual protected void Start()
    {
        
    }

    public void ReplaceDoorOpener(ConditionalDoorOpener doorOpener)
    {
        this.doorOpener = doorOpener;
    }
    public void ProgressDoorOpener()
    {
        doorOpener.ProgressOpening(this, true);
    }
    public override string ButtonPromptText()
    {
        return "";
    }
}
