using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Camera sceneCamera;
    [SerializeField]
    GameObject objectToFollow;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if(objectToFollow)
            sceneCamera.transform.position = new Vector3(objectToFollow.transform.position.x, sceneCamera.transform.position.y, objectToFollow.transform.position.z);
    }
}
