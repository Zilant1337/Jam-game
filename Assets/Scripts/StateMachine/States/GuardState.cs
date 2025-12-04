using System;
using UnityEngine;
using UnityEngine.AI;

public class GuardState : WanderState
{
    protected Vector3 startingPosition;

    public GuardState(EnemyScript enemyScript, NavMeshAgent agent, float wanderRadius) : base(enemyScript,agent,wanderRadius)
    {
        this.agent = agent;
        this.startingPosition = enemyScript.transform.position;
        this.wanderRadius = wanderRadius;
    }

    public override void OnEnter()
    {
        Debug.Log($"{enemyScript.gameObject.name} has entered the guard state");
    }

    public override void Update()
    {
        if (HasReachedDestination())
        {
            var randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += startingPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection,out hit,wanderRadius,1);
            var finalPosition = hit.position;
            agent.SetDestination(finalPosition);

        }
    }
}
