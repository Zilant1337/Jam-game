using UnityEngine;

public class SwitchDoorOpenerHelper : DoorOpenerHelper
{
    public override void InteractAction(Collider other)
    {
        ProgressDoorOpener();
    }
}
