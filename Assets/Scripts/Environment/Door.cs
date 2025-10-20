using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Transform doorModel;
    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    float timeToOpen;
    float openingTimer;
    bool isOpen;
    bool isMoving = false;
    public bool IsOpen { get => isOpen; }

    public bool Open()
    {
        if (isMoving)
        {
            return false;
        }
        isMoving = true;
        return true;
    }
    void Start()
    {
        openingTimer = 0;
    }

    
    void Update()
    {
        if (isMoving)
        {
            openingTimer += Time.deltaTime;
            if(openingTimer>=timeToOpen)
            {
                openingTimer = 0;
                isMoving = false;
                isOpen = !isOpen;
                doorModel.position = isOpen ? endPoint.position:startPoint.position;
                doorModel.rotation = isOpen ? endPoint.rotation: startPoint.rotation;
            }
            else
            {
                doorModel.position = Vector3.Lerp(isOpen ? endPoint.position : startPoint.position, isOpen ? startPoint.position : endPoint.position, openingTimer/timeToOpen);
                doorModel.rotation = Quaternion.Lerp(isOpen ? endPoint.rotation : startPoint.rotation, isOpen ? startPoint.rotation : endPoint.rotation, openingTimer / timeToOpen);
            }
        }
    }
}
