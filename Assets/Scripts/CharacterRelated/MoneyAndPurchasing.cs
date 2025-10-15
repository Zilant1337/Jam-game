using TMPro;
using UnityEngine;

public class MoneyAndPurchasing : MonoBehaviour
{
    public static MoneyAndPurchasing instance;
    [SerializeField]
    protected TMP_Text moneyUI;
    [SerializeField]
    protected int money;
    [SerializeField]
    protected int MAX_MONEY;

    private void Start()
    {
        if (instance!= null)
        {
            Debug.LogError("Can't have more than one MoneyAndPurchasing");
            Destroy(this);
            return;
        }
        instance = this;
        moneyUI.text = money.ToString();
    }

    public void AddMoney (int moneyToAdd)
    {
        if (money+moneyToAdd > MAX_MONEY)
        {
            if(money!=MAX_MONEY)
            {
                money = MAX_MONEY;
                moneyUI.text = money.ToString();
            }
            return;
        }
        money += moneyToAdd;
        moneyUI.text = money.ToString();
    }
    public bool RemoveMoney(int moneyToRemove)
    {
        if (moneyToRemove > money)
        {
            money -= moneyToRemove;
            moneyUI.text = money.ToString();
            return true;
        }
        return false;
    }
}
