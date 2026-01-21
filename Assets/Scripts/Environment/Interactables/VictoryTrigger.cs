using UnityEngine;

public class VictoryTrigger : Interactable
{
    public override void InteractAction(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            MenuScript.Instance.Victory();
    }
}