using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    protected LookAndShoot shootingScript;
    protected NavMeshAgent agent;
    protected Transform player;
    protected bool attacked;
    protected float attackCooldown;
    public bool Attacked { get => attacked; private set => attacked = value; }
    public AttackState(EnemyScript enemyScript, NavMeshAgent agent, LookAndShoot shootingScript, Transform player):base(enemyScript)
    {
        this.agent = agent;
        this.shootingScript = shootingScript;
        this.player = player;
        attacked = false;
    }
    public override void OnExit()
    {
        Attacked = false;
        enemyScript.AttackTimer = enemyScript.AttackCooldown;
    }
    public override void Update()
    {
        if (!attacked)
        {
            
        }
    }
}

