using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UniversalProgressBar : MonoBehaviour
{
    [SerializeField]
    SlicedFilledImage progressBar;
    [SerializeField]
    TMP_Text progressBarText;
    [SerializeField]
    List<GameObject> uiElementsToHide;
    public static UnityEvent<float> onProgress;
    public static UnityEvent onFinish;
    public static UnityEvent<string> onStart;
    protected bool isActive;
    protected void Start()
    {
        onProgress = new UnityEvent<float>();
        onProgress.AddListener(UpdateProgressBar);
        onFinish = new UnityEvent();
        onFinish.AddListener(HideProgressBar);
        onStart = new UnityEvent<string>();
        onStart.AddListener(ShowProgressBar);
        HideProgressBar();
    }
    protected void Update()
    {

    }
    protected void HideProgressBar()
    {
        this.progressBarText.text = "";
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }
    protected void ShowProgressBar(string progressBarText)
    {
        if(!isActive)
        {
            this.progressBarText.text = progressBarText;
            foreach (GameObject uiElement in uiElementsToHide)
            {
                uiElement.SetActive(true);
            }
        }
    }
    protected void UpdateProgressBar(float fraction)
    {
        progressBar.fillAmount = fraction;
    }
}
