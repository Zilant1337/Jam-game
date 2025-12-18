using UnityEngine;

public class BaseTrackingStrategy : ITrackingStrategy
{
    protected Transform tracker;
    protected Transform player;
    protected float trackingSpeed;
    protected bool isTracking;

    public BaseTrackingStrategy(Transform tracker, Transform player, float trackingSpeed)
    {
        this.isTracking = false;
        this.tracker = tracker;
        this.player = player;
        this.trackingSpeed = trackingSpeed;
    }

    public void Update()
    {
        if (isTracking)
        {
            Vector3 targetDirection = player.position - tracker.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            tracker.rotation = Quaternion.RotateTowards(tracker.rotation, targetRotation, trackingSpeed* Time.deltaTime);
        }
    }
    public void StartTracking()
    {
        isTracking = true;
    }
    public void StopTracking()
    {
        isTracking = false;
    }
}
