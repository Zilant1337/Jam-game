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
        targetProgress = new List<bool>(targets.Count);
        for (int i =0; i < targetProgress.Count; i++)
        {
            targetProgress[i] = false;
        }
    }
    protected void ActivateTargets()
    {
        foreach (var target in targets)
        {
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
        DeactivateTargets();
        base.Update();

    }
    public virtual void ProgressOpening(Target target)
    {
        // Если открыватель, который открывает дверь, есть в списке - ставим или снимаем флаг
        int targetndex = targets.IndexOf(target);
        if (targetndex != -1)
        {
            targetProgress[targetndex] = true;
        }
        // Если все флаги активированы, открываем дверь
        if (!targetProgress.Contains(false))
        {
            doorOpener.ProgressOpening(this, true);
            DeactivateTargets();    
        }
    }
    public override void InteractAction(Collider other)
    {
        return;
    }

}

public class Target : MonoBehaviour
{
    [SerializeField]
    TargetHealth health;
    [SerializeField]
    TimedTargetDoorOpenerHelper openerHelper;
    public void OnDeath()
    {
        openerHelper.ProgressOpening(this);
    }
    public void Activate()
    {

    }

    public void Deactivate()
    {

    }
}
