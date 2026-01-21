using UnityEngine;

public class FollowerScript : MonoBehaviour
{
    [SerializeField]
    float destroyDelay;
    Transform transformToFollow;
    bool isInitiated;
    float destroyTimer;
    public Transform TransformToFollow { get => transformToFollow; set => transformToFollow = value; }

    void Start()
    {
        destroyTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitiated)
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyDelay)
            {
                Destroy(gameObject);
            }
        }
        if (transformToFollow)
        {
            this.transform.position = transformToFollow.position;
            if (!isInitiated)
            {
                isInitiated = true;
            }
        }
        if(!transformToFollow && isInitiated)
        {
            Destroy(gameObject);
        }
    }
}
