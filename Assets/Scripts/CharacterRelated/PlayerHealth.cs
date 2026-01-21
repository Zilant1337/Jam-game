using UnityEngine;


public class PlayerHealth:Health
{
    [SerializeField]
    protected float invinsibilityTime;
    protected float invinsibilityTimer;
    protected override void Start()
    {
        base.Start();
        invinsibilityTimer = 0;
    }
    protected override void Update()
    {
        if (invinsibilityTimer > 0)
        {
            invinsibilityTimer -= Time.deltaTime;
            if (invinsibilityTimer <= 0)
            {
                invinsibilityTimer = 0;
            }
        }
    }
    public override void TakeDamage(float damage)
    {
        if(invinsibilityTimer==0)
        {
            base.TakeDamage(damage);
            healthBarScript.UpdateProgressBar(hp / MAX_HP);
            invinsibilityTimer = invinsibilityTime;
        }
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        MenuScript.Instance.GameOver();
    }
}
