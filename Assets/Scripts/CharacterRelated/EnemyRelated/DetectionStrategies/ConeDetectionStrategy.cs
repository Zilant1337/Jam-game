using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ConeDetectionStrategy : IDetectionStrategy
{
    private float detectionAngle;
    private float detectionRadius;
    private float guaranteedDetectionRadius;


    public ConeDetectionStrategy(float detectionAngle, float detectionRadius, float guaranteedDetectionRadius)
    {
        this.detectionAngle = detectionAngle;
        this.detectionRadius = detectionRadius;
        this.guaranteedDetectionRadius = guaranteedDetectionRadius;
    }
    public bool Execute(Transform player, Transform detector, LayerMask layerMask)
    {
        Vector3 directionToPlayer = player.position-detector.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);
        bool obstructed = false;
        RaycastHit hit;
        if (Physics.Raycast(detector.position, player.position, out hit, float.MaxValue, layerMask))
        {
            obstructed = true;
        }
        if ((!(angleToPlayer < detectionAngle / 2f) || !(directionToPlayer.magnitude < detectionRadius))
            && !(directionToPlayer.magnitude < guaranteedDetectionRadius) || obstructed)
        {
            Debug.Log("Can't see player");
            return false;
        }

        return true;

    }
}