using UnityEngine;

public class TargetHealth : Health
{
    [SerializeField]
    Target target;
    protected override void OnDeath()
    {
        hp = MAX_HP;
        target.OnDeath();
    }
}