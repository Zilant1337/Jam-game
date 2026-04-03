using UnityEngine;

public class TimedSwitchDoorOpenerHelper : TimedDoorOpenerHelper
{
    public override void InteractAction(Collider other)
    {
        if (timer != 0)
        {
            timer = 0;
            if (hideWhenInactive)
            {
                meshRenderer.enabled = false;
            }
            ProgressDoorOpener();
        }
    }
    public override string ButtonPromptText()
    {
        if (timer!=0)
            return $"Press the switch!";
        else 
            return "";
    }
}
