using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EnemyHealth : Health
{
    public override void TakeDamage(float damage)
    { 
        base.TakeDamage(damage);
        healthBarScript.UpdateProgressBar(hp / MAX_HP);
        GetComponent<EnemyScript>().TookDamageRecently = true;
    }
    protected override void OnDeath()
    {
        if(!isDead)
        {
            isDead = true;
            Destroy(this.gameObject);
            EnemyManager.instance.onEnemyDeath.Invoke(this.GetComponent<EnemyScript>().EnemyType, this.transform);
        }
    }
}


