
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]
public class EnemyScript : CharacterScript   
{
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    [SerializeField]
    protected PlayerDetector playerDetector;
    [SerializeField]
    private EnemyManager.EnemyType enemyType;

    StateMachine stateMachine;

    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    private void Awake()
    {
            
    }
    void Start()
    {
        stateMachine = new StateMachine();
        GuardState guardState = new GuardState(this, navMeshAgent, 10);
        Debug.Log($"Attaching player to enemy: {playerDetector.Player}");
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));

        stateMachine.SetState(guardState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
