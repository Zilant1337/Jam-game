using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField]
    int peletsPerShot;
    public override void Shoot()
    {
        if (ammoCountInMag > 0 && cooldownTimer == 0 && reloadTimer == 0)
        {
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            // Запуск визуального эффекта вспышки при выстреле
            muzzleFlashParticleSystem.Play();
            // Несколько выстрелов за раз
            for (int i = 0;i<peletsPerShot;i++)
            {
                TrailRenderer trailRenderer = Instantiate(bulletTracerRenderer, tracerEmmiterPoint.position, Quaternion.identity);
                Vector3 shotDirection = GetShotDirection();
                RaycastHit hit;
                if (Physics.Raycast(tracerEmmiterPoint.position, shotDirection, out hit, float.MaxValue, hitLayerMask))
                {
                    StartCoroutine(SpawnBulletTrail(trailRenderer, hit));
                    hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
                }
                else
                {
                    StartCoroutine(SpawnBulletTrail(trailRenderer, shotDirection));
                }
            }
            ammoCountInMag--;
            AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
            cooldownTimer = timeBetweenShots;
        }
    }
}
