using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    LineRenderer laserRenderer;

    public LineRenderer LaserRenderer { get => laserRenderer; }

    private void Start()
    {
        laserRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerMask))
        {
            laserRenderer.SetPosition(1, new Vector3(0,0,(transform.position-hit.point).magnitude));
        }
    }
}
