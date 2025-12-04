using UnityEngine;

public interface ITrackingStrategy
{
    bool Execute(Transform player, Transform tracker);
}
public class BaseTrackingStrategy : ITrackingStrategy
{
    protected Transform player;
    protected Transform enemy;
    protected float trackingSpeed;
    public bool Execute(Transform player, Transform tracker)
    {
        throw new System.NotImplementedException();
    }

}
