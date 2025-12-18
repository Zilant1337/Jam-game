using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    protected EnemyWeaponManager shootingScript;
    protected NavMeshAgent agent;
    protected Transform player;
    protected bool attacked;
    protected float attackCooldown;
    protected float shootAngle;
    public AttackState(EnemyScript enemyScript, NavMeshAgent agent, EnemyWeaponManager shootingScript, Transform player, float shootAngle):base(enemyScript)
    {
        this.agent = agent;
        this.shootingScript = shootingScript;
        this.player = player;
        this.shootAngle = shootAngle;
        attacked = false;
    }
    public override void OnEnter()
    {
        Debug.Log("Entered Attack state");
        enemyScript.NavMeshAgent.updateRotation = false;
        enemyScript.NavMeshAgent.isStopped = true;
        enemyScript.TrackingStrategy.StartTracking();
    }
    public override void OnExit()
    {
        enemyScript.NavMeshAgent.updateRotation = true;
        enemyScript.NavMeshAgent.isStopped = false;
        enemyScript.TrackingStrategy.StopTracking();
    }
    public bool checkAngle()
    {
        Vector3 directionToPlayer = player.position - enemyScript.transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, enemyScript.transform.forward);

        if (!(angleToPlayer < shootAngle / 2f))
        {
            return false;
        }
        return true;
    }
    public override void Update()
    {
        if (enemyScript.AttackTimer == 0 && checkAngle())
        {
            if (shootingScript.Shoot())
                enemyScript.AttackTimer = enemyScript.AttackCooldown;
        }
    }
    public override void FixedUpdate()
    {
        
    }
}

