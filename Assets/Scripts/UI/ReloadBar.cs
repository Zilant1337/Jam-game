using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReloadBar : MonoBehaviour
{
    [SerializeField]
    SlicedFilledImage reloadBar;
    [SerializeField]
    List<GameObject> uiElementsToHide;
    public static UnityEvent <float> onReload;
    public static UnityEvent onReloadEnd;
    public static UnityEvent onReloadStart;
    private void Start()
    {
        onReload = new UnityEvent <float> ();
        onReload.AddListener(UpdateReloadBar);
        onReloadEnd = new UnityEvent ();
        onReloadEnd.AddListener(HideReloadBar);
        onReloadStart = new UnityEvent ();
        onReloadStart.AddListener(ShowReloadBar);
        HideReloadBar();
    }
    private void Update()
    {

    }
    private void HideReloadBar()
    {
        foreach(GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }
    private void ShowReloadBar()
    {
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(true);
        }
    }
    private void UpdateReloadBar(float fraction)
    {
        reloadBar.fillAmount = fraction;
    }
}
