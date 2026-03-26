using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int m_MAX_HP = 100;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected ProgressBar healthBarScript;
    protected bool isDead = false;

    public bool IsFull { get => hp == MAX_HP; }
    public int MAX_HP { get => m_MAX_HP;}

    protected virtual void Start()
    {
        hp = MAX_HP;
        if (healthBarScript != null)
            healthBarScript.UpdateProgressBar(hp / MAX_HP);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        // Функция, отвечающая за получение урона и смерть, если здоровье закончилось
        if(hp - damage > 0)
        {
            hp -= damage;
            if (healthBarScript != null)
                healthBarScript.UpdateProgressBar(hp / MAX_HP);
        }
        else
        {
            hp = 0;
            if (healthBarScript != null)
                healthBarScript.UpdateProgressBar(hp / MAX_HP);
            OnDeath();
        }
    }
    public virtual void AddHealth(float health)
    {
        if (hp + health < MAX_HP)
        {
            hp += health;
            if (healthBarScript != null)
                healthBarScript.UpdateProgressBar(hp / MAX_HP);
        }
        else
        {
            hp = MAX_HP;
            if (healthBarScript != null)
                healthBarScript.UpdateProgressBar(hp / MAX_HP);
        }
    }
    protected virtual void OnDeath()
    {
        if(!isDead)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }
}
