using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class BulletPattern : MonoBehaviour
{
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
                    ShootNextVolley();
                    timer = volleyDelay[volleyCounter];
                }
                else
                {
                    volleyCounter = 0;
                    timer = volleyDelay[volleyCounter];
                    isShooting = false;
                }
            }
        }
    }
}
