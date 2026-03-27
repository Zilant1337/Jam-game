using UnityEngine;

public class SwitchDoorOpenerHelper : ConditionalDoorOpenerHelper
{
    public override void InteractAction(Collider other)
    {
        ProgressDoorOpener();
    }
}
