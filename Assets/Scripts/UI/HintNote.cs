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
    
    private bool isMoving;
    private bool isOnScreen;

    public static HintNote Instance;

    private void Awake()
    {
        isMoving = false;
        isOnScreen = false;
        timer = 0;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (isOnScreen&& timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                }
                float curveValue = offScreenMovementCurve.Evaluate(1-timer/disappearanceTime);
                transform.position = Vector3.Lerp(noteOnScreenPosition.position,noteOOSPosition.position,curveValue);
                isOnScreen = false;
            }
            if (!isOnScreen && timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                }
                float curveValue = offScreenMovementCurve.Evaluate(1 - timer / appearanceTime);
                transform.position = Vector3.Lerp(noteOOSPosition.position, noteOnScreenPosition.position, curveValue);
                isOnScreen = true;
            }
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
}
