
using System.Collections.Generic;
using UnityEngine;

public class PatternGun : Gun
{
    [SerializeField]
    List<BulletPattern> bulletPatterns;
    [SerializeField]
    List<int> patternOrder;
    [SerializeField]
    List<float> patternDelays;
    protected int patternCounter;
    private void Start()
    {
        patternCounter = 0;
    }
    public override void Shoot()
    {
        if ((ammoCountInMag > 0 || hasInfiniteAmmo) && cooldownTimer == 0 && reloadTimer == 0)
        {
            float resetPitch = shootAudioSource.pitch;
            shootAudioSource.pitch += UnityEngine.Random.Range(-shootSoundPitchRange, shootSoundPitchRange);
            shootAudioSource.PlayOneShot(shootAudioSource.clip);
            shootAudioSource.pitch = resetPitch;

            muzzleFlashParticleSystem.Play();

            bulletPatterns[patternOrder[patternCounter]].Shoot();

            ammoCountInMag--;
            if (!hasInfiniteAmmo)
                AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
            patternCounter = (patternCounter+1)%patternOrder.Count;
            cooldownTimer = patternDelays[patternCounter];
        }
    }
}
