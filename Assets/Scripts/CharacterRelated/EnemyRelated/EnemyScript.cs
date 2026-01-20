
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
    protected EnemyManager.EnemyType enemyType;

    [SerializeField]
    protected float attackDistance;
    [SerializeField]
    protected float attackCooldown;
    [SerializeField] 
    protected float dashSpeed;
    [SerializeField]
    protected float dashAccelleration;
    [SerializeField]
    protected float dashDistance;
    [SerializeField]
    protected float dashCooldown;
    [SerializeField]
    protected float shootAngle;
    [SerializeField]
    protected float trackingSpeed;


    protected ITrackingStrategy trackingStrategy;
    protected float attackTimer;
    protected float dashTimer;
    protected StateMachine stateMachine;
    protected EnemySpawnerArea enemySpawnerArea;
    
    public float AttackCooldown { get => attackCooldown; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public float DashSpeed { get => dashSpeed; }
    public float DashAccelleration { get => dashAccelleration; }
    public float DashCooldown { get => dashCooldown; }
    public float DashTimer { get => dashTimer; set => dashTimer = value; }
    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    public ITrackingStrategy TrackingStrategy { get => trackingStrategy;}
    public NavMeshAgent NavMeshAgent { get => navMeshAgent; }
    public EnemySpawnerArea EnemySpawnerArea { get => enemySpawnerArea; set => SetSpawnerArea(value); }
    public bool TookDamageRecently { get; set; }

    protected virtual void Awake()
    {
            
    }
    protected virtual void Start()
    {
        TookDamageRecently = false;
        attackTimer = 0;
        dashTimer = 0;
        trackingStrategy = new BaseTrackingStrategy(transform, playerDetector.Player,trackingSpeed);
        stateMachine = new StateMachine();
        GuardState guardState = new GuardState(this, navMeshAgent, 10);
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        AttackState attackState = new AttackState(this, navMeshAgent, weaponManager, playerDetector.Player,shootAngle,true);

        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => TookDamageRecently));
        stateMachine.AddTransition(chaseState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()&&!TookDamageRecently));
        stateMachine.AddTransition(chaseState,attackState,new FunctionPredicate(() => playerDetector.CanDetectPlayer()
        &&Vector3.Distance(transform.position,playerDetector.Player.transform.position)<=attackDistance));
        stateMachine.AddTransition(attackState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()&& Vector3.Distance(transform.position, playerDetector.Player.transform.position) > attackDistance));
        stateMachine.AddAnyTransition(guardState,new FunctionPredicate(() => !playerDetector.Player));
        stateMachine.SetState(guardState);
        healthBarFollower.TransformToFollow = transform;
        healthBarFollower.transform.SetParent(null);
    }

    // Update is called once per frame
    public virtual void OnDeath()
    {

    }
    protected virtual void Update()
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
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashTimer = 0;
                Debug.Log($"{this.name} is ready to dash again");
            }
        }
        stateMachine.Update();
        trackingStrategy.Update();
    }
    protected virtual void SetSpawnerArea(EnemySpawnerArea enemySpawnerArea)
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
    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
