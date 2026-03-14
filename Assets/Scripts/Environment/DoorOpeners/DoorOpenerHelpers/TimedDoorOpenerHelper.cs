using UnityEngine;

public abstract class TimedDoorOpenerHelper : DoorOpenerHelper
{
    [SerializeField]
    protected float expirationTime;
    protected float timer;
    protected virtual void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                OnTimeOut();
                timer = 0;
            }
        }
    }
    protected virtual void OnTimeOut()
    {
        doorOpener.ProgressOpening(this, false);
    }
}
