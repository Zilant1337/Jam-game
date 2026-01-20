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



    public Transform Player { get; private set; }
    
    IDetectionStrategy detectionStrategy;
    private void Awake()
    {
        FindPlayer();
    }
    private void Start()
    {
        FindPlayer();
        detectionStrategy = new ConeDetectionStrategy(detectionAngle,detectionRadius,guaranteedDetectionRadius);
    }
    private void Update()
    {

    }
    public void FindPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public bool CanDetectPlayer()
    {
        return detectionStrategy.Execute(Player, transform, obstructionsLayerMask);
    }
    public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => this.detectionStrategy = detectionStrategy;

}
