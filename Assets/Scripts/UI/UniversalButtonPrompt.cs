using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class    UniversalButtonPrompt : MonoBehaviour
{
    [SerializeField]
    TMP_Text buttonPromptText;
    [SerializeField]
    List<GameObject> uiElements;
    [SerializeField]
    InputSystemActionPrompts.PromptText promptText;
    public static UnityEvent onFinish;
    public static UnityEvent<string> onStart;
    protected bool isActive;
    protected void Start()
    {
        onFinish = new UnityEvent();
        onFinish.AddListener(HideButtonPrompt);
        onStart = new UnityEvent<string>();
        onStart.AddListener(ShowButtonPrompt);
        HideButtonPrompt();
    }
    protected void Update()
    {

    }
    protected void HideButtonPrompt()
    {
        this.buttonPromptText.text = "";
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }
    }
    protected void ShowButtonPrompt(string buttonPromptText)
    {
        if (!isActive)
        {
            this.buttonPromptText.text = buttonPromptText;
            foreach (GameObject uiElement in uiElements)
            {
                uiElement.SetActive(true);
            }
            promptText.RefreshText();
        }
    }
}
