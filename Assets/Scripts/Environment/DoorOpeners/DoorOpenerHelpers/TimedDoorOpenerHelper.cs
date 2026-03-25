using NUnit.Framework;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class TimedDoorOpenerHelper : DoorOpenerHelper
{
    [SerializeField]
    protected string timerText;
    [SerializeField]
    protected float expirationTime;
    protected float timer;
    
    public virtual void OnActivation()
    {
        timer = expirationTime;
        UniversalProgressBar.onStart.Invoke(timerText);
        UniversalProgressBar.onProgress.Invoke(timer / expirationTime);
        isActivated = false;
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
        doorOpener.ProgressOpening(this, false);
    }
}
