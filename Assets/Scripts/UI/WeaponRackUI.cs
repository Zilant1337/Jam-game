using TMPro;
using UnityEngine;

public class WeaponRackUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text weaponRackText;
    public void UpdateWeaponRackText(Weapon weapon)
    {
        weaponRackText.text = $"{weapon.WeaponName}\n{weapon.Price}";
    }
}
