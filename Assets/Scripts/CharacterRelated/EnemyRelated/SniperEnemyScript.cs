using Unity.VisualScripting;
using UnityEngine;

public class SniperEnemyScript : EnemyScript
{
    [SerializeField]
    protected float firingDelay;
    [SerializeField]
    protected Laser laser;
    protected override void Start()
    {
        TookDamageRecently = false;
        attackTimer = 0;
        dashTimer = 0;
        trackingStrategy = new BaseTrackingStrategy(transform, playerDetector.Player, trackingSpeed);
        stateMachine = new StateMachine();
        GuardState guardState = new GuardState(this, navMeshAgent, 10);
        ChaseState chaseState = new ChaseState(this, navMeshAgent, playerDetector.Player);
        SniperAttackState attackState = new SniperAttackState(this, navMeshAgent, weaponManager, playerDetector.Player, shootAngle,firingDelay, true, laser);

        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(guardState, chaseState, new FunctionPredicate(() => TookDamageRecently));
        stateMachine.AddTransition(chaseState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer() && !TookDamageRecently));
        stateMachine.AddTransition(chaseState, attackState, new FunctionPredicate(() => playerDetector.CanDetectPlayer()
        && Vector3.Distance(transform.position, playerDetector.Player.transform.position) <= attackDistance));
        stateMachine.AddTransition(attackState, guardState, new FunctionPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FunctionPredicate(() => playerDetector.CanDetectPlayer() && Vector3.Distance(transform.position, playerDetector.Player.transform.position) > attackDistance));
        stateMachine.AddAnyTransition(guardState, new FunctionPredicate(() => !playerDetector.Player));
        stateMachine.SetState(guardState);
        healthBarFollower.TransformToFollow = transform;
        healthBarFollower.transform.SetParent(null);
    }
}
