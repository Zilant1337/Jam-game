using UnityEngine;

public class PaidInteractable : Interactable
{
    [SerializeField]
    protected int price;
    [SerializeField]
    protected PaidInteractableUI paidInteractableUI;
    public int Price { get => price; }

    public override void InteractAction(Collider other)
    {
        if(CheckReadiness())
        {
            if (MoneyAndPurchasing.instance.RemoveMoney(Price))
            {
                Debug.Log($"Removed {Price}");
                PaidAction();
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
        else
        {
            Debug.Log($"Not ready!");
        }
    }
    protected virtual void PaidAction()
    {

    }
    protected virtual bool CheckReadiness()
    {
        return false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
