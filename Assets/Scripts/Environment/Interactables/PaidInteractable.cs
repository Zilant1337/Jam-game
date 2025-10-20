using UnityEngine;

public class PaidInteractable : Interactable
{
    [SerializeField]
    protected int price;
    [SerializeField]
    protected PaidInteractableUI paidInteractableUI;
    public int Price { get => price; }

    public override void InteractAction()
    {
        if (MoneyAndPurchasing.instance.RemoveMoney(Price))
        {
            Debug.Log($"Removed {Price}");
            PaidAction();
        }
        else
        {
            Debug.Log($"Not enough money!");
        }
    }
    protected virtual void PaidAction()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
