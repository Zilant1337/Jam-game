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
                if (!PaidAction())
                {
                    Refund();
                }
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
    protected virtual bool PaidAction()
    {
        return false;
    }
    protected virtual bool CheckReadiness()
    {
        return false;
    }
    protected virtual void Refund()
    {
        MoneyAndPurchasing.instance.AddMoney(Price);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ButtonPromptText()
    {
        return $"Buy for {price}";
    }
}
