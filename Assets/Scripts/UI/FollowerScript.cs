using UnityEngine;

public class FollowerScript : MonoBehaviour
{
    Transform transformToFollow;
    bool isInitiated;
    public Transform TransformToFollow { get => transformToFollow; set => transformToFollow = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
