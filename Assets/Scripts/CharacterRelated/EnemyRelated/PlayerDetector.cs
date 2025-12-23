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
    protected LayerMask obstructionsLayerMask;

    float timer;

    public Transform Player { get; private set; }
    
    IDetectionStrategy detectionStrategy;
    private void Awake()
    {
        FindPlayer();
    }
    private void Start()
    {
        timer = 0;
        FindPlayer();
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
    public void FindPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public bool CanDetectPlayer()
    {
        return timer == 0 && detectionStrategy.Execute(Player, transform, obstructionsLayerMask);
    }
    public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => this.detectionStrategy = detectionStrategy;

}
