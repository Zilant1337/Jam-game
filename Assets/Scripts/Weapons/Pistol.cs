using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        if (ammoCountInMag > 0 && cooldownTimer ==0 && reloadTimer == 0)
        {
            // Выстрел с помощью кастинга лучей
            muzzleFlashParticleSystem.Play();
            RaycastHit hit;
            Vector3 shotDirection = GetShotDirection();

            TrailRenderer trailRenderer = Instantiate(bulletTracerRenderer, tracerEmmiterPoint.position, Quaternion.identity);
            
            if (Physics.Raycast(transform.position, shotDirection, out hit, float.MaxValue, hitLayerMask))
            {
                StartCoroutine(SpawnBulletTrail(trailRenderer, hit));
                hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
            }
            else
            {
                StartCoroutine(SpawnBulletTrail(trailRenderer,shotDirection));
            }
                ammoCountInMag--;
            AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag,ammoCount);
            cooldownTimer = timeBetweenShots;
        }
    }
}
