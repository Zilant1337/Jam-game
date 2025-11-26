using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InstaDamageGun : Gun
{
    [SerializeField]
    protected List<string> tagsToHit;
    [SerializeField]
    protected List<string> tagsToIgnore;



    protected List<Health> healthsToDamage = new List<Health>();
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && !healthsToDamage.Contains(health))
        {
            healthsToDamage.Add(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            healthsToDamage.Remove(health);
        }
    }
    protected virtual void DealDamage(List<Health> healthList)
    {
        foreach (Health health in healthList)
        {
            if (health != null && ToDamage(health))
                health.TakeDamage(damagePerBullet);
        }
    }
    protected bool ToDamage(Health other)
    {
        // Проводится проверка тегов объектов, которым должен наноситься урон
        bool toDamage = false;
        foreach (string tag in tagsToHit)
        {
            if (other.gameObject.CompareTag(tag))
            {
                toDamage = true;
                break;
            }
        }
        if (toDamage)
        {
            foreach (string tag2 in tagsToIgnore)
            {
                if (other.gameObject.CompareTag(tag2))
                {
                    toDamage = false;
                    break;
                }
            }
        }
        return toDamage;
    }
    public override void Shoot()
    {
        if((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer == 0 && reloadTimer == 0)
        {
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            foreach (Health health in healthsToDamage)
            {
                if (health != null)
                    health.TakeDamage(damagePerBullet);
            }
            cooldownTimer = timeBetweenShots;
            // Отнимаем количество патронов в магазине
            ammoCountInMag--;
            // Вызываем событие обновления интерфейса
            if (!hasInfiniteAmmo)
                AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
        }
    }
}
