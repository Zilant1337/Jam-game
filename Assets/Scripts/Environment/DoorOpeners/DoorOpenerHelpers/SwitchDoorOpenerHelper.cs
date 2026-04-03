using UnityEngine;

public class SwitchDoorOpenerHelper : ConditionalDoorOpenerHelper
{
    public override void InteractAction(Collider other)
    {
        ProgressDoorOpener();
    }
    public override string ButtonPromptText()
    {
        return $"Press the switch";
    }
}
