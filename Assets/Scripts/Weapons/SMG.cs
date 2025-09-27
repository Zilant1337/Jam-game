using UnityEngine;

public class SMG : Weapon
{
    public override void Shoot()
    {
        // Выстрел через кастинг лучей
        if (ammoCountInMag > 0 && cooldownTimer == 0 && reloadTimer == 0)
        {
            // Запуск визуального эффекта вспышки при выстреле
            muzzleFlashParticleSystem.Play();
            RaycastHit hit;
            Vector3 shotDirection = GetShotDirection();

            // Инициализация трейсера для видимости куда ушёл выстрел
            TrailRenderer trailRenderer = Instantiate(bulletTracerRenderer, tracerEmmiterPoint.position, Quaternion.identity);

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
            // Отнимаем количество патронов в магазине
            ammoCountInMag--;
            // Вызываем событие обновления интерфейса
            AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
            // Запуск таймера для ограничения скорострельности
            cooldownTimer = timeBetweenShots;
        }
    }
}
