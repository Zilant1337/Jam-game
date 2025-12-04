using UnityEngine;


public class ConeDetectionStrategy : IDetectionStrategy
{
    private float detectionAngle;
    private float detectionRadius;
    private float guaranteedDetectionRadius;


    public ConeDetectionStrategy(float detectionAngle, float detectionRadius, float guaranteedDetectionRadius)
    {
        //Угол конуса
        this.detectionAngle = detectionAngle;
        // Радиус засечения в конусе
        this.detectionRadius = detectionRadius;
        // Радиус гарантированного засечения
        this.guaranteedDetectionRadius = guaranteedDetectionRadius;
    }
    public bool Execute(Transform player, Transform detector, LayerMask layerMask)
    {
        Vector3 directionToPlayer = player.position-detector.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);
        bool obstructed = false;
        RaycastHit hit;
        if (Physics.Linecast(detector.position + Vector3.up, player.position + Vector3.up, out hit, layerMask))
        {
            obstructed = true;
        }
        // Если игрок не входит в конус зрения, не находится достаточно близко к противнику или загорожен препятствием, говорим что игрока не видно
        if ((!(angleToPlayer < detectionAngle / 2f) || !(directionToPlayer.magnitude < detectionRadius))
            && !(directionToPlayer.magnitude < guaranteedDetectionRadius) || obstructed)
        {
            return false;
        }

        return true;

    }
}