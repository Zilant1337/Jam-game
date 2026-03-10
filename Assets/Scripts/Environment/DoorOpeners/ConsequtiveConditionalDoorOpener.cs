using UnityEngine;

public class ConsequtiveConditionalDoorOpener : ConditionalDoorOpener
{
    public void ResetProgress()
    {
        foreach(DoorOpenerHelper helper in openerHelpers)
        {
            helper.IsActivated = false;
        }
        for(int i = 0; i < openingProgress.Count;i++)
        {
            openingProgress[i] = false;
        }
    }
}
