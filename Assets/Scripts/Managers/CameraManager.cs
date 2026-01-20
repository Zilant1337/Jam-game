using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField]
    Camera sceneCamera;
    [SerializeField]
    GameObject objectToFollow;

    public Camera SceneCamera { get => sceneCamera; }

    void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Can't have more than 2 camera managers!");
        }
    }
    public bool CheckObjectVisibility(GameObject gameObject)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(sceneCamera);
        return planes.All(plane => plane.GetDistanceToPoint(gameObject.transform.position) >= 0);
    }
    public bool CheckObjectVisibility(Vector3 objectPosition)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(sceneCamera);
        return planes.All(plane => plane.GetDistanceToPoint(objectPosition) >= 0);
    }
    void LateUpdate()
    {
        if(objectToFollow)
            sceneCamera.transform.position = new Vector3(objectToFollow.transform.position.x, sceneCamera.transform.position.y, objectToFollow.transform.position.z);
    }
}
