using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerHealth:Health
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBarScript.UpdateHealthBar(hp / MAX_HP);
    }
}
