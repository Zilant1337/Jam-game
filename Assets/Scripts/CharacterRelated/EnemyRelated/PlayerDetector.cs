using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    float detectionAngle = 60;
    [SerializeField]
    float detectionRadius = 10;
    [SerializeField]
    float guaranteedDetectionRadius = 5;
    [SerializeField]
    float detectionCooldown = 1;

    float timer;

    public Transform Player { get; private set; }
    
    IDetectionStrategy detectionStrategy;
    private void Start()
    {
        timer = 0;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        detectionStrategy = new ConeDetectionStrategy(detectionAngle,detectionRadius,guaranteedDetectionRadius);
    }
    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                timer = 0;
            }
        }
    }
    public bool CanDetectPlayer()
    {
        return timer == 0 && detectionStrategy.Execute(Player,transform);
    }
    public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => this.detectionStrategy = detectionStrategy;

}
