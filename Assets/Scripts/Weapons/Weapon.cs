using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string weaponName;
    [SerializeField]
    protected Sprite previewImage;
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
    [SerializeField]
    protected float reloadTime;
    protected float reloadTimer;

    public int AmmoCountInMag { get => ammoCountInMag;}
    public int AmmoCount { get => ammoCount;}
    public string WeaponName { get => weaponName;}
    public Sprite PreviewImage { get => previewImage;}

    protected void Start()
    {
        ammoCount = MAX_AMMO;
        ammoCountInMag = magSize;
        cooldownTimer = 0;
        reloadTimer = 0;
    }
    protected void Update()
    {
        // Таймер для ограничения темпа стрельбы
        if (cooldownTimer!=0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }
        if (reloadTimer != 0)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                FillAmmo();
                AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag,ammoCount);
                reloadTimer = 0;
            }
        }
    }

    public abstract void Shoot();
    public void Reload()
    {
        reloadTimer = reloadTime;
    }
    private void FillAmmo()
    {
        if (magSize - ammoCountInMag < ammoCount)
        {
            ammoCount -= magSize - ammoCountInMag;
            ammoCountInMag = magSize;
        }
        else
        {
            ammoCountInMag += ammoCount;
            ammoCount = 0;
        }
    }
    public void Discard()
    {
        // Выброс оружия
        transform.parent = worldWeaponParent.transform;
    }
    public void PickUp(CharacterScript character)
    {

    }
}