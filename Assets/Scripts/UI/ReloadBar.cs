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
    protected void Start()
    {
        onReload = new UnityEvent <float> ();
        onReload.AddListener(UpdateReloadBar);
        onReloadEnd = new UnityEvent ();
        onReloadEnd.AddListener(HideReloadBar);
        onReloadStart = new UnityEvent ();
        onReloadStart.AddListener(ShowReloadBar);
        HideReloadBar();
    }
    protected void Update()
    {

    }
    protected void HideReloadBar()
    {
        foreach(GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }
    protected void ShowReloadBar()
    {
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(true);
        }
    }
    protected void UpdateReloadBar(float fraction)
    {
        reloadBar.fillAmount = fraction;
    }
}
