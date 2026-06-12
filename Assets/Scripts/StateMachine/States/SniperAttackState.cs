using UnityEngine;
using UnityEngine.AI;

public class SniperAttackState : AttackState
{
    protected float delay;
    protected float delayTimer;
    protected Laser laser;
    public SniperAttackState(EnemyScript enemyScript, NavMeshAgent agent, EnemyWeaponManager shootingScript, Transform player, float shootAngle, float delay, bool stopWhileAttacking, Laser laser) : base(enemyScript, agent, shootingScript, player, shootAngle, stopWhileAttacking)
    {
        delayTimer = -1;
        this.agent = agent;
        this.shootingScript = shootingScript;
        this.player = player;
        this.shootAngle = shootAngle;
        this.delay = delay;
        this.stopWhileAttacking = stopWhileAttacking;
        attacked = false;
        this.laser = laser;
    }
    public override void OnEnter()
    {
        delayTimer = delay;
        laser.LaserRenderer.enabled = true;
        base.OnEnter();
    }
    public override void OnExit()
    {
        laser.LaserRenderer.enabled = false;
        base.OnExit();
    }
    public override void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer-= Time.deltaTime;
            if (delayTimer < 0)
            {
                delayTimer = 0;
            }
        }
        if (enemyScript.AttackTimer == 0 && delayTimer == 0 && checkAngle())
        {
            if (shootingScript.Shoot())
                enemyScript.AttackTimer = enemyScript.AttackCooldown;
        }
    }
}
