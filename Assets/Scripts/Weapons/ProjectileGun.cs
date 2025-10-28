using UnityEngine;

public class ProjectileGun : Weapon
{
    [SerializeField]
    Transform projectilePrefabTransform;
    public override void Shoot()
    {
        if (ammoCountInMag > 0 && cooldownTimer == 0 && reloadTimer == 0)
        {
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            muzzleFlashParticleSystem.Play();

            Vector3 shotDirection = GetShotDirection();

            Instantiate(projectilePrefabTransform,tracerEmmiterPoint.position,Quaternion.LookRotation(shotDirection,Vector3.up));

            // Отнимаем количество патронов в магазине
            ammoCountInMag--;
            // Вызываем событие обновления интерфейса
            AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
            // Запуск таймера для ограничения скорострельности
            cooldownTimer = timeBetweenShots;
        }
    }
}
