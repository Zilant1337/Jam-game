using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    SlicedFilledImage staminaBar;
    private void Update()
    {

    }
    public void UpdateStaminaBar(float fraction)
    {
        staminaBar.fillAmount = fraction;
    }
}
