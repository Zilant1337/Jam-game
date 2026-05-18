using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HintNote : MonoBehaviour
{
    [SerializeField]
    TMP_Text noteText;
    [SerializeField]
    private Transform noteOOSPosition;
    [SerializeField]
    private Transform noteOnScreenPosition;

    public static HintNote Instance;

    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Can't have more than one HintNote. {Instance.gameObject.name} already exists");
        }
    }

    public void ChangeText(string newText)
    {
        noteText.text = newText;
    }
}
