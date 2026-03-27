using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    TimedTargetDoorOpenerHelper openerHelper;

    public TimedTargetDoorOpenerHelper OpenerHelper { get => openerHelper; set => SetDoorOpenerHelper(value); }
    protected void SetDoorOpenerHelper(TimedTargetDoorOpenerHelper openerHelper)
    {
        if (this.openerHelper == null)
        {
            this.openerHelper = openerHelper;
        }
        else
        {
            Debug.LogError($"Can't add another door opener to {this.name}");
        }
        return;
    }

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
