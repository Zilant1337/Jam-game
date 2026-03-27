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
        foreach(ConditionalDoorOpenerHelper helper in openerHelpers)
        {
            helper.IsActivated = false;
        }
        for(int i = 0; i < openingProgress.Count;i++)
        {
            openingProgress[i] = false;
        }
        UniversalProgressBar.onFinish.Invoke();
    }
    
    public override void ProgressOpening(ConditionalDoorOpenerHelper helper, bool isSuccessful)
    {
        Debug.Log($"Helper {helper.name} is trying to progress door opening");
        int openerIndex = openerHelpers.IndexOf(helper);
        if (isSuccessful && openerIndex != -1 && openerIndex == currentId)
        {
            Debug.Log($"Door opening progress successful!");
            openingProgress[openerIndex] = true;
            currentId++;
            if (currentId < openerHelpers.Count)
            {
                Debug.Log($"Activating {openerHelpers[currentId].name}");
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
        else
        {
            Debug.Log($"Door opening progress failed!");
            ResetProgress();
            return;
            
        }
        
    }

}
