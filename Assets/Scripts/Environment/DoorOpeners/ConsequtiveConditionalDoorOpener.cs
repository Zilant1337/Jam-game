using NUnit.Framework;
using UnityEngine;

public class ConsequtiveConditionalDoorOpener : ConditionalDoorOpener
{
    protected int currentId;
    protected override void Start()
    {
        base.Start();
        currentId = 0;
    }
    public void ResetProgress()
    {
        currentId = 0;
        foreach(DoorOpenerHelper helper in openerHelpers)
        {
            helper.IsActivated = false;
        }
        for(int i = 0; i < openingProgress.Count;i++)
        {
            openingProgress[i] = false;
        }
        UniversalProgressBar.onFinish.Invoke();
    }
    
    public override void ProgressOpening(DoorOpenerHelper helper, bool isSuccessful)
    {
        if (!isSuccessful)
        {
            ResetProgress();
            return;
        }
        int openerIndex = openerHelpers.IndexOf(helper);
        if (isSuccessful && openerIndex!=-1 && openerHelpers.IndexOf(helper)==currentId)
        {
            if (openerIndex != currentId)
            {
                ResetProgress();
                return;
            }
            openingProgress[openerIndex] = !openingProgress[openerIndex];
            currentId++;
            if (currentId < openerHelpers.Count)
            {
                UniversalProgressBar.onFinish.Invoke();
                openerHelpers[currentId].IsActivated = true;
            }
            if (!openingProgress.Contains(false))
            { 
                door.Open();
                ResetProgress();
            }
            return;
        }
        
    }

}
