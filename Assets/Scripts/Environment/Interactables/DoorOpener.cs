using UnityEngine;

public class DoorOpener : PaidInteractable
{
    [SerializeField]
    Door door;
    protected override void PaidAction()
    {
        door.Open();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paidInteractableUI.UpdatePaidInteractableText("Open the door", price.ToString());
    }
    protected override bool CheckReadiness()
    {
        return !door.IsMoving;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
