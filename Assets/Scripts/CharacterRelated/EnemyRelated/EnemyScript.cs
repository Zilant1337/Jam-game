
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
        GuardState guardState = new GuardState(this,navMeshAgent,10);
        stateMachine.AddAnyTransition(guardState,new FunctionPredicate(()=>true));
        stateMachine.SetState(guardState);

    }
    void Start()
    {
        
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
