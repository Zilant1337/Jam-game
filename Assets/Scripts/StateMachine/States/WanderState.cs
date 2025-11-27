using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    protected NavMeshAgent agent;
    protected float wanderRadius;
    public WanderState(EnemyScript enemyScript, NavMeshAgent agent, float wanderRadius) : base(enemyScript)
    {
        this.agent = agent;
        this.wanderRadius = wanderRadius;
    }

    public override void FixedUpdate()
    {
    }

    public override void OnEnter()
    {
        Debug.Log($"{enemyScript.gameObject.name} has entered the wander state");
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
        if (HasReachedDestination())
        {
            var randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += enemyScript.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            var finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    protected bool HasReachedDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }
}
