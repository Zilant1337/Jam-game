using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected string gunName;
    [SerializeField]
    protected Sprite previewImage;
    [SerializeField]
    protected ParticleSystem muzzleFlashParticleSystem;
    [SerializeField]
    protected LayerMask hitLayerMask;
    [SerializeField]
    protected GameObject worldWeaponParent;
    [SerializeField]
    protected Transform tracerEmmiterPoint;

    [SerializeField]
    protected int price;
    [SerializeField]
    protected int m_MAX_AMMO;
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
    [SerializeField]
    protected float shotSpread;
    [SerializeField]
    protected TrailRenderer bulletTracerRenderer;
    [SerializeField]
    protected float tracerTravelTime;
    [SerializeField]
    protected bool isInteractable;
    [SerializeField]
    protected bool isAutomatic;
    [SerializeField]
    protected bool hasInfiniteAmmo;



    [SerializeField]
    protected AudioSource reloadAudioSource;
    [SerializeField]
    protected AudioSource shootAudioSource;
    [SerializeField]
    protected float shootSoundPitchRange;

    public int Price { get => price; }
    public int AmmoCountInMag { get => ammoCountInMag; set => ammoCountInMag = value; }
    public int AmmoCount { get => ammoCount; set => ammoCount = value>MAX_AMMO?MAX_AMMO:value; }
    public string GunName { get => gunName;}
    public Sprite PreviewImage { get => previewImage;}
    public bool IsAutomatic { get => isAutomatic;}
    public float ReloadTimer { get => reloadTimer; }
    public int MAX_AMMO { get => m_MAX_AMMO; }
    public int MagSize { get => magSize; }
    public bool HasInfiniteAmmo { get => hasInfiniteAmmo; }

    protected void Awake()
    {
        ammoCount = MAX_AMMO;
        ammoCountInMag = magSize;
        cooldownTimer = 0;
        reloadTimer = 0;
    }
    protected virtual void Update()
    {
        // Таймер для ограничения темпа стрельбы
        ProgressCooldown();
        // Таймер для ограничения стрельбы и смены оружия при перезарядки
        ProgressReloadTimer();
    }
    protected virtual void ProgressCooldown()
    {
        if (cooldownTimer != 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }
    }
    protected virtual void ProgressReloadTimer()
    {
        if (reloadTimer != 0)
        {
            ReloadBar.onReload.Invoke(1 - reloadTimer / reloadTime);
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                FillAmmo();
                ReloadBar.onReloadEnd.Invoke();
                AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag, ammoCount);
                reloadTimer = 0;
            }
        }
    }
    // Реализация выстрела у каждого оружия разная
    public abstract bool Shoot();
    // Запускаем таймер для перезарядки
    public void Reload()
    {
        if (ammoCountInMag != magSize && ammoCount!=0&&!hasInfiniteAmmo)
        {
            ReloadBar.onReloadStart.Invoke();
            reloadTimer = reloadTime;
            // При перезарядке запускается звук
            reloadAudioSource.Play();
        }
    }
    // Заполнение магазина и отъём восполненного из общего пула патронов для оружия
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
    
    protected Vector3 GetShotDirection()
    {
        // Добавляем случайный разброс при стрельбе
        Vector3 direction = tracerEmmiterPoint.forward;
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-shotSpread, shotSpread), Vector3.up);
        direction = rotation * direction;
        return direction.normalized;
    }
    // Процедуры запуска трейсера от выстрела
    protected IEnumerator SpawnBulletTrail(TrailRenderer trailRenderer, Vector3 direction)
    {
        float timer = 0;
        Vector3 startPosition = trailRenderer.transform.position;
        Vector3 endPosition = startPosition + direction * 100;
        while (timer < tracerTravelTime)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition,endPosition,timer);
            timer+=Time.deltaTime/trailRenderer.time;
            yield return null;
        }
        Destroy(trailRenderer.gameObject,trailRenderer.time);
    }
    protected IEnumerator SpawnBulletTrail(TrailRenderer trailRenderer, RaycastHit hit)
    {
        float timer = 0;
        Vector3 startPosition = trailRenderer.transform.position;
        while (timer < tracerTravelTime)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition, hit.point, timer*100);
            timer += Time.deltaTime / trailRenderer.time;
            yield return null;
        }
        Destroy(trailRenderer.gameObject, trailRenderer.time);
    }
}