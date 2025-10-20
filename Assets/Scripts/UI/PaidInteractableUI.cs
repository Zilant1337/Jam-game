using TMPro;
using UnityEngine;

public class PaidInteractableUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text paidInteractableText;
    public void UpdatePaidInteractableText(string name, string price)
    {
        paidInteractableText.text = $"{name}\n{price}";
    }
}
