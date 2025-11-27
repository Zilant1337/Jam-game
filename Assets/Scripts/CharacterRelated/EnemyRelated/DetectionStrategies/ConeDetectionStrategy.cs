using Unity.VisualScripting;
using UnityEngine;

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
    public bool Execute(Transform player, Transform detector)
    {
        Vector3 directionToPlayer = player.position-detector.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);
        if((!(angleToPlayer<detectionAngle/2f)||!(directionToPlayer.magnitude<detectionRadius))
            &&!(directionToPlayer.magnitude<guaranteedDetectionRadius))
            return false;

        return true;

    }
}