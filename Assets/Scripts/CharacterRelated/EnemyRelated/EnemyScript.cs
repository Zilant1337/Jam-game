
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]
public class EnemyScript : MonoBehaviour  
{
    [SerializeField]
    protected EnemyWeaponManager weaponManager;
    [SerializeField]
    protected FollowerScript healthBarFollower;
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
    [SerializeField]
    float shootAngle;
    [SerializeField]
    float trackingSpeed;


    ITrackingStrategy trackingStrategy;
    float attackTimer;
    StateMachine stateMachine;
    EnemySpawnerArea enemySpawnerArea;
    
    public float AttackCooldown { get => attackCooldown; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    public ITrackingStrategy TrackingStrategy { get => trackingStrategy;}
    public NavMeshAgent NavMeshAgent { get => navMeshAgent; }
    public EnemySpawnerArea EnemySpawnerArea { get => enemySpawnerArea; set => SetSpawnerArea(value); }
    public bool TookDamageRecently { get; set; }

    private void Awake()
    {
            
    }
    void Start()
    {
        TookDamageRecently = false;
        attackTimer = 0;
        trackingStrategy = new BaseTrackingStrategy(transform, playerDetector.Player,trackingSpeed);
        stateMachine = new StateMachine();
        GuardState guardState = new GuardState(this, navMeshAgent, 10);
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        AttackState attackState = new AttackState(this, navMeshAgent, weaponManager, playerDetector.Player,shootAngle);

        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()&&!TookDamageRecently));
        stateMachine.AddTransition(chaseState,attackState,new FunctionPredicate(() => playerDetector.CanDetectPlayer()
        &&Vector3.Distance(transform.position,playerDetector.Player.transform.position)<=attackDistance));
        stateMachine.AddTransition(attackState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()&& Vector3.Distance(transform.position, playerDetector.Player.transform.position) > attackDistance));
        stateMachine.AddAnyTransition(guardState,new FunctionPredicate(() => !playerDetector.Player));
        stateMachine.AddAnyTransition(chaseState, new FunctionPredicate(() => TookDamageRecently));
        stateMachine.SetState(guardState);
        healthBarFollower.TransformToFollow = transform;
        healthBarFollower.transform.SetParent(null);
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
        trackingStrategy.Update();
    }
    void SetSpawnerArea(EnemySpawnerArea enemySpawnerArea)
    {
        if (!this.enemySpawnerArea)
        {
            this.enemySpawnerArea = enemySpawnerArea;
        }
        else
        {
            Debug.LogError($"Tried to assign a different spawner area to {gameObject.name}");
        }
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
