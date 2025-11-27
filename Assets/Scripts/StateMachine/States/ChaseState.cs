using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    protected NavMeshAgent agent;
    protected Transform player;
    public ChaseState(EnemyScript enemyScript, NavMeshAgent agent, Transform player) : base(enemyScript)
    {
        this.agent = agent;
        this.player = player;
    }

    public override void FixedUpdate()
    {

    }

    public override void OnEnter()
    {
        Debug.Log($"{enemyScript.name} has entered the chase state");
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        agent.SetDestination(player.position);
    }
}

