
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : CharacterScript   
{
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    [SerializeField]
    private EnemyManager.EnemyType enemyType;

    StateMachine stateMachine;

    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    private void Awake()
    {
        stateMachine = new StateMachine();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
