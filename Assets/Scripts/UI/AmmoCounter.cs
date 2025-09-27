using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField]
    TMP_Text ammoInMag;
    [SerializeField]
    TMP_Text ammoTotal;
    [SerializeField]
    TMP_Text weaponName;
    [SerializeField]
    Image weaponPreview;

    public static UnityEvent <int, int> ammoCountChanged;
    public static UnityEvent<int, int, string, Sprite> weaponChanged;

    private void Awake()
    {
        // Инициализация событий и назначение им функций 
        ammoCountChanged = new UnityEvent <int,int>();
        weaponChanged = new UnityEvent<int,int,string,Sprite>();
        ammoCountChanged.AddListener(UpdateAmmoCounterNumbers);
        weaponChanged.AddListener(UpdateAmmoCounterFull);
    }
    public void UpdateAmmoCounterNumbers(int ammoInMag,int ammoTotal)
    {
        // Обновление только чисел, показывающие количество патронов в магазине и в кармане
        this.ammoInMag.text = ammoInMag.ToString();
        this.ammoTotal.text = ammoTotal.ToString();
    }
    public void UpdateAmmoCounterFull(int ammoInMag, int ammoTotal, string weaponName, Sprite weaponPreview)
    {
        // Обновление чисел патронов в магазине, названия и изображения оружия в интерфейсе
        UpdateAmmoCounterNumbers(ammoInMag, ammoTotal);
        this.weaponName.text = weaponName;
        this.weaponPreview.sprite = weaponPreview;
    }
}
