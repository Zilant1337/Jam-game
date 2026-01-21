using UnityEngine;
using UnityEngine.AI;

public class DashState : BaseState
{
    protected NavMeshAgent agent;
    protected Transform player;
    protected float normalSpeed;
    protected float normalAccelleration;
    protected bool trackWhileDashing;
    protected float maxDashDistance;
    protected float dashedDistance;
    protected Vector3 lastPosition;
    public DashState(EnemyScript enemyScript, NavMeshAgent agent, Transform player, bool trackWhileDashing, float maxDashDistance) : base(enemyScript)
    {
        this.agent = agent;
        this.player = player;
        normalSpeed = agent.speed;
        normalAccelleration = agent.acceleration;
        this.trackWhileDashing = trackWhileDashing;
        this.maxDashDistance = maxDashDistance;
        dashedDistance = 0;
        Vector3 vector3 = Vector3.zero;
    }

    public override void FixedUpdate()
    {
    }

    public override void OnEnter()
    { 
        Debug.Log($"{enemyScript.gameObject.name} has entered the dash state");
        agent.speed = enemyScript.DashSpeed;
        agent.acceleration = enemyScript.DashAccelleration;
        lastPosition = enemyScript.transform.position;
        agent.SetDestination(player.position);
    }

    public override void OnExit()
    {
        agent.speed = normalSpeed;
        agent.acceleration = normalAccelleration;
        agent.isStopped = true;
        agent.isStopped = false;
        dashedDistance = 0;
    }

    public override void Update()
    {
        if (trackWhileDashing)
        {
            agent.SetDestination(player.position);
        }
        dashedDistance += enemyScript.DashSpeed*Time.deltaTime;
        lastPosition = enemyScript.transform.position;
        if (!agent.hasPath||dashedDistance>=maxDashDistance)
        {

            enemyScript.DashTimer = enemyScript.DashCooldown;
        }
    }
}

