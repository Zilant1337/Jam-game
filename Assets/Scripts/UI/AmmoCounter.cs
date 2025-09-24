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

    private void Awake()
    {
        ammoCountChanged = new UnityEvent <int,int>();
        ammoCountChanged.AddListener(UpdateAmmoCounterNumbers);
    }
    public void UpdateAmmoCounterNumbers(int ammoInMag,int ammoTotal)
    {
        this.ammoInMag.text = ammoInMag.ToString();
        this.ammoTotal.text = ammoTotal.ToString();
    }
    public void UpdateAmmoCounterFull(int ammoInMag, int ammoTotal, string weaponName, Sprite weaponPreview)
    {
        UpdateAmmoCounterNumbers(ammoInMag, ammoTotal);
        this.weaponName.text = weaponName;
        this.weaponPreview.sprite = weaponPreview;
    }
}
