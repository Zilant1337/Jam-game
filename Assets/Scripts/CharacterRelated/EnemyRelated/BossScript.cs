
using UnityEngine;

public class BossScript : EnemyScript
{
    [SerializeField]
    protected SwitchDoorOpenerHelper bossDoorOpenerHelper;

    public SwitchDoorOpenerHelper BossDoorOpenerHelper { get => bossDoorOpenerHelper; set => bossDoorOpenerHelper = value; }

    protected override void Start()
    {
        TookDamageRecently = false;
        attackTimer = 0;
        dashTimer = 0;
        trackingStrategy = new BaseTrackingStrategy(transform, playerDetector.Player, trackingSpeed);
        stateMachine = new StateMachine();
        WanderState wanderState = new WanderState(this, navMeshAgent, 10);
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        AttackState attackState = new AttackState(this, navMeshAgent, weaponManager, playerDetector.Player, shootAngle,false);
        DashState dashState = new DashState(this, navMeshAgent, playerDetector.Player,true,dashDistance);

        stateMachine.AddTransition(wanderState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(wanderState, chaseState, new FunctionPredicate(() => TookDamageRecently));
        stateMachine.AddTransition(chaseState, dashState, new FunctionPredicate(() => playerDetector.CanDetectPlayer() && dashTimer == 0));
        stateMachine.AddTransition(dashState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer() && dashTimer != 0));
        stateMachine.AddTransition(dashState, wanderState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer() && dashTimer != 0));
        stateMachine.AddTransition(chaseState, wanderState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer() && !TookDamageRecently));
        stateMachine.AddTransition(chaseState, attackState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()
        && Vector3.Distance(transform.position, playerDetector.Player.transform.position) <= attackDistance));
        stateMachine.AddTransition(attackState, wanderState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer() && attackTimer != 0));
        stateMachine.AddTransition(attackState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer() && 
        (Vector3.Distance(transform.position, playerDetector.Player.transform.position) > attackDistance || attackTimer!=0)));
        stateMachine.AddAnyTransition(wanderState, new FunctionPredicate(() => !playerDetector.Player));
        stateMachine.SetState(wanderState);
        healthBarFollower.TransformToFollow = transform;
        healthBarFollower.transform.SetParent(null);
    }
    public override void OnDeath()
    {
        if(bossDoorOpenerHelper)
            bossDoorOpenerHelper.InteractAction(GetComponent <CapsuleCollider>());
    }
}
