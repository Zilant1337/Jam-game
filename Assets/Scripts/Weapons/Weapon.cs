using System.Collections;
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
    protected Transform tracerEmmiterPoint;
    
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

    public int AmmoCountInMag { get => ammoCountInMag;}
    public int AmmoCount { get => ammoCount;}
    public string WeaponName { get => weaponName;}
    public Sprite PreviewImage { get => previewImage;}
    public bool IsAutomatic { get => isAutomatic;}
    public float ReloadTimer { get => reloadTimer; }

    protected void Awake()
    {
        ammoCount = MAX_AMMO;
        ammoCountInMag = magSize;
        cooldownTimer = 0;
        reloadTimer = 0;
    }

    protected void Start()
    {
        
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
        // Таймер для ограничения стрельбы и смены оружия при перезарядки
        if (reloadTimer != 0)
        {
            ReloadBar.onReload.Invoke(1-reloadTimer/reloadTime);
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                FillAmmo();
                ReloadBar.onReloadEnd.Invoke();
                AmmoCounter.ammoCountChanged.Invoke(ammoCountInMag,ammoCount);
                reloadTimer = 0;
            }
        }
    }
    // Реализация выстрела у каждого оружия разная
    public abstract void Shoot();
    // Запускаем таймер для перезарядки
    public void Reload()
    {
        if (ammoCountInMag != magSize)
        {
            ReloadBar.onReloadStart.Invoke();
            reloadTimer = reloadTime;
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
    public void PickUp(CharacterScript character)
    {

    }
    protected Vector3 GetShotDirection()
    {
        // Добавляем случайный разброс при стрельбе
        Vector3 direction = tracerEmmiterPoint.forward;
        Debug.DrawRay(tracerEmmiterPoint.position,direction,Color.red,5);
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-shotSpread, shotSpread), Vector3.up);
        direction = rotation * direction;
        Debug.DrawRay(tracerEmmiterPoint.position, direction, Color.black, 5);
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