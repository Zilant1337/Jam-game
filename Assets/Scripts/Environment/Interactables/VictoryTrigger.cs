using UnityEngine;

public class VictoryTrigger : Interactable
{
    public override void InteractAction(Collider other)
    {
        MenuScript.Instance.Victory();
    }
}