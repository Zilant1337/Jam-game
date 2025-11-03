using UnityEngine;

public class ProjectileGun : Gun
{
    [SerializeField]
    Transform projectilePrefabTransform;
    public override void Shoot()
    {
        if ((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer == 0 && reloadTimer == 0)
        {
            float resetPitch = shootAudioSource.pitch;
            shootAudioSource.pitch += UnityEngine.Random.Range(-shootSoundPitchRange, shootSoundPitchRange);
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            shootAudioSource.pitch = resetPitch;
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
