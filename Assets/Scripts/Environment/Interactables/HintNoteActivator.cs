using UnityEngine;

public class HintNoteActivator : MonoBehaviour
{
    [SerializeField]
    private float noteDelay;
    [SerializeField]
    private string noteText;
    float timer;
    bool fired;
    private void Awake()
    {
        timer = 0;
        fired = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("DefaultGuy")&& timer == 0 && !fired)
        {
            timer = noteDelay;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.Find("DefaultGuy"))
        {
            timer = 0;
            HintNote.Instance.RemoveNote();
        }
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                fired = true;
                HintNote.Instance.ReplaceNote(noteText);
            }
        }
    }

}
