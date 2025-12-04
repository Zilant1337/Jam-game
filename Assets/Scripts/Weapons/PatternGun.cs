
using System.Collections.Generic;
using UnityEngine;

public class PatternGun : Gun
{
    // Список паттернов, которые можно применять в определённом порядке
    [SerializeField]
    List<BulletPattern> bulletPatterns;
    // Порядок паттернов
    [SerializeField]
    List<int> patternOrder;
    // Задержки после отстрела каждого паттерна
    [SerializeField]
    List<float> patternDelays;
    protected int patternCounter;
    protected bool finishedShooting;
    private void Start()
    {
        patternCounter = 0;
        finishedShooting = true;
    }
    public override bool Shoot()
    {
        if ((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer == 0 && reloadTimer == 0)
        {
            float resetPitch = shootAudioSource.pitch;
            shootAudioSource.pitch += UnityEngine.Random.Range(-shootSoundPitchRange, shootSoundPitchRange);
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            shootAudioSource.pitch = resetPitch;

            muzzleFlashParticleSystem.Play();

            bulletPatterns[patternOrder[patternCounter]].Shoot();

            finishedShooting = false;
            ammoCountInMag--;
            patternCounter = (patternCounter+1)%patternOrder.Count;
            cooldownTimer = patternDelays[patternCounter];
            return true;
        }
        return false;
    }
    protected override void ProgressCooldown()
    {
        if (finishedShooting)
        {
            base.ProgressCooldown();
        }
    }
    public void FinishPattern()
    {
        finishedShooting = true;
    }
}
