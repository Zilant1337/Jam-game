using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HitscanGun : Weapon
{
    [SerializeField]
    protected bool piercing;
    public override void Shoot()
    {
        // Выстрел через кастинг лучей
        if (ammoCountInMag > 0 && cooldownTimer ==0 && reloadTimer == 0)
        {
            // Запуск визуального эффекта вспышки при выстреле
            muzzleFlashParticleSystem.Play();
            
            Vector3 shotDirection = GetShotDirection();

            // Инициализация трейсера для видимости куда ушёл выстрел
            TrailRenderer trailRenderer = Instantiate(bulletTracerRenderer, tracerEmmiterPoint.position, Quaternion.identity);
            if (piercing)
            {
                List<RaycastHit> hits = new List<RaycastHit>();
                hits = Physics.RaycastAll(transform.position, shotDirection, float.MaxValue, hitLayerMask)?.ToList();
                StartCoroutine(SpawnBulletTrail(trailRenderer, shotDirection));
                foreach (RaycastHit hit in hits)
                {
                    hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
                }
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, shotDirection, out hit, float.MaxValue, hitLayerMask))
                {
                    // Пуск трейсера
                    StartCoroutine(SpawnBulletTrail(trailRenderer, hit));
                    // Получение урона тем, в кого попала пуля
                    hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
                }
                else
                {
                    // Пуск трейсера
                    StartCoroutine(SpawnBulletTrail(trailRenderer, shotDirection));
                }
            }
            
            // Отнимаем количество патронов в магазине
            ammoCountInMag--;
            // Вызываем событие обновления интерфейса
            AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag,ammoCount);
            // Запуск таймера для ограничения скорострельности
            cooldownTimer = timeBetweenShots;
        }
    }
}
