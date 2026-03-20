using UnityEngine;

public class TimedSwitchDoorOpenerHelper : TimedDoorOpenerHelper
{
    public override void InteractAction(Collider other)
    {
        if (timer != 0)
        {
            timer = 0;

            ProgressDoorOpener();
        }
    }
}
