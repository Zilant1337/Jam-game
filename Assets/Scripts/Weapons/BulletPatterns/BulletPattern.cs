using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class BulletPattern : MonoBehaviour
{
    [SerializeField]
    protected PatternGun gun;
    // Углы, под которыми выпускаются снаряды
    [SerializeField]
    protected List<float> volleyAngles;
    // Задержка перед выпуском следующего снаряда
    [SerializeField]
    protected List<float> volleyDelay;

    protected int volleyCounter;
    protected float timer;
    protected bool isShooting;

    protected abstract void ShootNextVolley();
    public void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
        }
    }

    protected void Start()
    {
        isShooting = false;
        volleyCounter = 0;
        timer = volleyDelay[volleyCounter];
    }
    protected void Update()
    {
        if(isShooting)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                    timer = 0;
            }
            if (timer == 0)
            {
                // Пока не закончился отстрел паттерна, производим выстрелы
                if (volleyCounter < volleyAngles.Count)
                {
                    Debug.Log("Shooting next volley");
                    ShootNextVolley();
                    if(volleyCounter<volleyAngles.Count)
                        timer = volleyDelay[volleyCounter];
                }
                else
                {
                    Debug.Log("Volley ended");
                    volleyCounter = 0;
                    timer = volleyDelay[volleyCounter];
                    isShooting = false;
                    gun.FinishPattern();
                }
            }
        }
    }
}
