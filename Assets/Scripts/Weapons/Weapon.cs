using UnityEngine;
using UnityEngine.Rendering;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem muzzleFlashParticleSystem;
    [SerializeField]
    protected LayerMask hitLayerMask;
    [SerializeField]
    protected GameObject worldWeaponParent;
    [SerializeField]
    protected bool isInteractable;
    [SerializeField]
    protected int MAX_AMMO;
    protected int ammoCount;
    [SerializeField]
    protected int magSize;
    protected int ammoCountInMag;
    [SerializeField]
    protected float damagePerBullet;
    [SerializeField]
    protected float timeBetweenShots;
    protected float cooldownTimer;

    protected void Start()
    {
        ammoCount = MAX_AMMO;
        ammoCountInMag = magSize;
        cooldownTimer = 0;
    }
    protected void Update()
    {
        if (cooldownTimer!=0)
        {
            cooldownTimer-= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }
    }

    public abstract void Shoot();
    public void Reload()
    {
        if (magSize - ammoCountInMag < ammoCount)
        {
            ammoCountInMag = magSize;
            ammoCount -= magSize - ammoCountInMag;
        }
        else
        {
            ammoCountInMag += ammoCount;
            ammoCount = 0;
        }
    }
    public void Discard()
    {
        transform.parent = worldWeaponParent.transform;
    }
    public void PickUp(CharacterScript character)
    {

    }
}