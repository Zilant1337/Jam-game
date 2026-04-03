using System.Collections.Generic;
using UnityEngine;

public class TimedTargetDoorOpenerHelper : TimedDoorOpenerHelper
{
    [SerializeField]
    protected List<Target> targets;
    [SerializeField]
    protected List<bool> targetProgress;
    protected override void Start()
    {
        base.Start();
        targetProgress = new List<bool>(new bool[targets.Count]);
        for (int i =0; i < targetProgress.Count; i++)
        {
            targetProgress[i] = false;
        }
        foreach (Target target in targets)
        {
            target.OpenerHelper = this;
        }
    }
    public override void OnActivation()
    {
        base.OnActivation();
        ActivateTargets();
    }
    protected void ActivateTargets()
    {
        foreach (var target in targets)
        {
            Debug.Log($"Activating {target.name}");
            target.Activate();
        }
    }
    protected void DeactivateTargets()
    {
        foreach (var target in targets)
        {
            target.Deactivate();
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    public virtual void ProgressOpening(Target target)
    {
        // Если цель, которая открывает дверь, есть в списке - ставим или снимаем флаг
        int targetIndex = targets.IndexOf(target);
        Debug.Log($"Target index: {targetIndex}");
        if (targetIndex != -1)
        {
            targetProgress[targetIndex] = true;
        }
        // Если все флаги активированы, открываем дверь
        if (!targetProgress.Contains(false))
        {
            doorOpener.ProgressOpening(this, true);
            DeactivateTargets();
            for (int i = 0; i < targetProgress.Count; i++)
            {
                targetProgress[i] = false;
            }
        }
    }
    public override void InteractAction(Collider other)
    {
        return;
    }
    public override string ButtonPromptText()
    {
        return "";
    }
}
