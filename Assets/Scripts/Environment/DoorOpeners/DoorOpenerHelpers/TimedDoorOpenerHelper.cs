using NUnit.Framework;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class TimedDoorOpenerHelper : ConditionalDoorOpenerHelper
{
    [SerializeField]
    protected string timerText;
    [SerializeField]
    protected float expirationTime;
    [SerializeField]
    protected bool hideWhenInactive = false;
    [SerializeField]
    protected MeshRenderer meshRenderer;
    protected float timer;
    
    public virtual void OnActivation()
    {
        if (hideWhenInactive)
        {
            meshRenderer.enabled = true;
        }
        timer = expirationTime;
        UniversalProgressBar.onStart.Invoke(timerText);
        UniversalProgressBar.onProgress.Invoke(timer / expirationTime);
        isActivated = false;
    }
    protected override void Start()
    {
        base.Start();
        if (hideWhenInactive)
        {
            meshRenderer.enabled = false;
        }
    }
    protected virtual void Update()
    {
        if (timer == 0 && isActivated)
        {
            OnActivation();
            
        }
        if (timer > 0 && !isActivated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                UniversalProgressBar.onProgress.Invoke(timer / expirationTime);
                timer = 0;
                OnTimeOut();
                return;
            }
            UniversalProgressBar.onProgress.Invoke(timer/expirationTime);
        }
    }
    protected virtual void OnTimeOut()
    {
        if (hideWhenInactive)
        {
            meshRenderer.enabled = false;
        }
        doorOpener.ProgressOpening(this, false);
    }
}
