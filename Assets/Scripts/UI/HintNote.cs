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
    [SerializeField]
    private AnimationCurve toScreenMovementCurve;
    [SerializeField]
    private AnimationCurve offScreenMovementCurve;
    [SerializeField]
    private float appearanceTime;
    [SerializeField]
    private float disappearanceTime;

    private float timer;
    
    private bool isOnScreen;
    private bool isChanging;
    private string replacementText;

    public static HintNote Instance;

    private void Awake()
    {
        isOnScreen = false;
        isChanging = false;
        timer = 0;
    }

    private void Update()
    {

        if (isOnScreen&& timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isOnScreen = false;
                if (isChanging&& replacementText!=null)
                {
                    ChangeText(replacementText);
                    replacementText = null;
                    isChanging = false;
                    timer = appearanceTime;
                }
            }
            float curveValue = offScreenMovementCurve.Evaluate(1-timer/disappearanceTime);
            transform.position = Vector3.Lerp(noteOnScreenPosition.position,noteOOSPosition.position,curveValue);
                
        }
        if (!isOnScreen && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isOnScreen = true;
            }
            float curveValue = offScreenMovementCurve.Evaluate(1 - timer / appearanceTime);
            transform.position = Vector3.Lerp(noteOOSPosition.position, noteOnScreenPosition.position, curveValue);
        }
    }

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
    // Returns if replacement was successfully initiated
    public bool ReplaceNote(string newText)
    {
        if (newText != noteText.text && timer == 0)
        {
            if (isOnScreen)
            {
                timer = disappearanceTime;
                replacementText = newText;
                isChanging = true;
            }
            else
            {
                ChangeText(newText);
                timer = appearanceTime; 
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RemoveNote()
    {
        if (isOnScreen)
        {
            if(timer == 0)
                timer = disappearanceTime;
            replacementText = null;
            isChanging = false;
            return true;
        }
        return false;
    }
}
