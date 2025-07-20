using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int MAX_HP = 100;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected HealthBar healthBarScript;

    void Start()
    {
        hp = MAX_HP;
        healthBarScript.UpdateHealthBar(hp / MAX_HP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        if(hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            Destroy(this.gameObject);
        }
        healthBarScript.UpdateHealthBar(hp / MAX_HP);
    }
}
