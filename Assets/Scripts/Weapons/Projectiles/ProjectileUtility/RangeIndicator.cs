using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;

public class RangeIndicator : MonoBehaviour
{
    [SerializeField]
    protected Transform indicatorTransform;
    private Transform objectToFollow;

    public Transform ObjectToFollow { get => objectToFollow; set => objectToFollow = value; }

    void Start()
    {
        
    }
    public void SetSize(float radius)
    {
        indicatorTransform.localScale = new Vector3(radius, indicatorTransform.localScale.y, radius);
    }
    // Update is called once per frame
    void Update()
    {
        if (ObjectToFollow)
            this.transform.position = ObjectToFollow.position;
        else
            Destroy(this.gameObject);
    }
}
