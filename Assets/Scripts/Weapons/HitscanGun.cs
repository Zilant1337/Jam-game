using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitscanGun : Gun
{
    [SerializeField]
    protected bool piercing;
    public override bool Shoot()
    {
        // Выстрел через кастинг лучей
        if ((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer ==0 && reloadTimer == 0)
        {
            float resetPitch = shootAudioSource.pitch;
            shootAudioSource.pitch += UnityEngine.Random.Range(-shootSoundPitchRange, shootSoundPitchRange);
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            shootAudioSource.pitch = resetPitch;
            // Запуск визуального эффекта вспышки при выстреле
            if(muzzleFlashParticleSystem)
                muzzleFlashParticleSystem.Play();
            
            Vector3 shotDirection = GetShotDirection();

            // Инициализация трейсера для видимости куда ушёл выстрел
            TrailRenderer trailRenderer = Instantiate(bulletTracerRenderer, tracerEmmiterPoint.position, Quaternion.identity);
            // Если выстрел должен быть пробивающим, то наносим урон всем, кто попался в луч
            if (piercing)
            {
                bool shouldFlyThrough = true;
                List<RaycastHit> hits = new List<RaycastHit>();
                // Сортируем попадания пробивающего рейкаста по расстоянию
                var hitsArray = Physics.RaycastAll(transform.position, shotDirection, float.MaxValue, hitLayerMask, QueryTriggerInteraction.Collide);
                Array.Sort(hitsArray, (x, y) => x.distance.CompareTo(y.distance));
                
                hits = hitsArray?.ToList();

                List<GameObject> hitObjects = new List<GameObject>();

                // Проходимся по всем объектам в которые попал пробивающий рейкаст
                foreach (RaycastHit hit in hits)
                {
                    // Проверяем не попадали ли мы уже в этот объект
                    if(!hitObjects.Find(x => x==hit.transform.gameObject))
                    {
                        // Если у объекта есть здоровье, он должен получить урон
                        if (hit.transform.GetComponent<Health>())
                            hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
                        // Если объект - припятствие, то дальнейшие объекты не должны проверяться
                        if (hit.transform.gameObject.layer == 7)
                        {
                            shouldFlyThrough = false;
                            StartCoroutine(SpawnBulletTrail(trailRenderer, hit));
                            break;
                        }
                        hitObjects.Add(hit.transform.gameObject);
                    }
                }
                if (shouldFlyThrough)
                    StartCoroutine(SpawnBulletTrail(trailRenderer, shotDirection));
            }
            // Если нет, наносим урон только тому, в кого попал первым
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, shotDirection, out hit, float.MaxValue, hitLayerMask, QueryTriggerInteraction.Collide))
                {
                    // Пуск трейсера до того, в кого попали
                    StartCoroutine(SpawnBulletTrail(trailRenderer, hit));
                    // Получение урона тем, в кого попала пуля
                    if (hit.transform.GetComponent<Health>())
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
            // Запуск таймера для ограничения скорострельности
            cooldownTimer = timeBetweenShots;
            return true;
        }
        return false;
    }
}
