using UnityEngine;

public class TimedObjectDestroyer : MonoBehaviour
{
    [SerializeField]
    protected float timeToLive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyObject()
    {
        Destroy(gameObject,timeToLive);
    }
}
