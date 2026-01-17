using UnityEngine;

public class ProjectileGun : Gun
{
    [SerializeField]
    protected Transform projectileRangeIndicatorPrefabTransform;
    [SerializeField]
    protected Transform projectilePrefabTransform;

    public override bool Shoot()
    {
        if ((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer == 0 && reloadTimer == 0)
        {
            float resetPitch = shootAudioSource.pitch;
            shootAudioSource.pitch += UnityEngine.Random.Range(-shootSoundPitchRange, shootSoundPitchRange);
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            shootAudioSource.pitch = resetPitch;
            if(muzzleFlashParticleSystem)
                muzzleFlashParticleSystem.Play();

            Vector3 shotDirection = GetShotDirection();

            var projectile = Instantiate(projectilePrefabTransform,tracerEmmiterPoint.position,Quaternion.LookRotation(shotDirection,Vector3.up));
            float explosionRadius = 0;
            if (projectilePrefabTransform.GetComponent<ExplosiveRocket>())
                explosionRadius= projectilePrefabTransform.GetComponent<ExplosiveRocket>().ExplosionRadius;

            if (projectileRangeIndicatorPrefabTransform)
            {
                var rangeIndicator = Instantiate(projectileRangeIndicatorPrefabTransform, tracerEmmiterPoint.position, Quaternion.identity);
                rangeIndicator.GetComponent<RangeIndicator>().SetSize(explosionRadius);
                rangeIndicator.GetComponent<RangeIndicator>().ObjectToFollow = projectile.transform;
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
