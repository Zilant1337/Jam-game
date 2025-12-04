
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]
public class EnemyScript : MonoBehaviour  
{
    [SerializeField]
    protected EnemyWeaponManager weaponManager;
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    [SerializeField]
    protected PlayerDetector playerDetector;
    
    [SerializeField]
    private EnemyManager.EnemyType enemyType;

    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackCooldown;

    float attackTimer;

    public float AttackCooldown { get => attackTimer; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }

    StateMachine stateMachine;

    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    private void Awake()
    {
            
    }
    void Start()
    {
        attackCooldown = 0;
        stateMachine = new StateMachine();
        GuardState guardState = new GuardState(this, navMeshAgent, 10);
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        AttackState attackState = new AttackState(this, navMeshAgent, weaponManager, playerDetector.Player);

        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState,attackState,new FunctionPredicate(() => playerDetector.CanDetectPlayer()
        &&Vector3.Distance(transform.position,playerDetector.Player.transform.position)<=attackDistance
        &&attackTimer==0));
        stateMachine.AddTransition(attackState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()&&attackState.Attacked));

        stateMachine.SetState(guardState);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer-= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                Debug.Log($"{this.name} is ready to shoot again");
            }
        }
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
