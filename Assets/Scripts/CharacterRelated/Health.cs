using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int MAX_HP = 100;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected HealthBar healthBarScript;
    protected bool isDead = false;

    void Start()
    {
        hp = MAX_HP;
        healthBarScript.UpdateHealthBar(hp / MAX_HP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        // Функция, отвечающая за получение урона и смерть, если здоровье закончилось
        if(hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
            OnDeath();
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
