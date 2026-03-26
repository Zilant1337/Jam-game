using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    TimedTargetDoorOpenerHelper openerHelper;
    public void OnDeath()
    {
        openerHelper.ProgressOpening(this);
        Deactivate();
    }
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
