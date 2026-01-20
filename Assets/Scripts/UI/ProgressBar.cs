using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    SlicedFilledImage progressBarFill;

    private void Update()
    {

    }
    public void UpdateProgressBar(float fraction)
    {
        progressBarFill.fillAmount = fraction;
    }
}
