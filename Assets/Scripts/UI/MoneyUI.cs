using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text moneyUIText;
    public static MoneyUI instance;
    public UnityEvent<int> onMoneyChange;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Can't have more than one MoneyUI");
            Destroy(this.gameObject);
        }
        instance = this;

        onMoneyChange = new UnityEvent<int>();
        onMoneyChange.AddListener(OnMoneyChange);
    }
    private void OnMoneyChange(int money)
    {
        moneyUIText.text = money.ToString();
    }
}
