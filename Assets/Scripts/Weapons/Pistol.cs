using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        if (ammoCountInMag > 0&& cooldownTimer ==0)
        {
            muzzleFlashParticleSystem.Play();
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, transform.position - transform.parent.position, out hit, float.MaxValue, hitLayerMask))
            {
                hit.transform.GetComponent<Health>().TakeDamage(damagePerBullet);
            }
            cooldownTimer = timeBetweenShots;
        }
    }
}
