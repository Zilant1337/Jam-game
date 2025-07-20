using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    SlicedFilledImage healthBar;
    private void Update()
    {
        
    }
    public void UpdateHealthBar(float fraction)
    {
        healthBar.fillAmount = fraction;
    }
}
