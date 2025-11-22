using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Interactable
{
    [SerializeField, Range(0f, 1f)]
    // Процент восстанавливаемых патронов в виде дроби
    float ammoRestored;
    [SerializeField]
    // Теги объектов, которым стоит восстанавливать патроны
    List<string> tagsToGiveAmmoTo;
    // Флаг для предотвращения двойного добавления патронов в процессе уничтожения объекта
    bool canGiveAmmo;
    public override void InteractAction(Collider other)
    {
        if (canGiveAmmo)
        {
            bool giveAmmo = false;
            // Проверяем нужно ли давать объекту патроны
            foreach (string tag in tagsToGiveAmmoTo)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    giveAmmo = true;
                    break;
                }
            }
            // Если нужно и у него есть LookAndShoot, говорим ему пополнить патроны и уничтожаем себя
            if (giveAmmo)
            {
                LookAndShoot guns = other.GetComponent<LookAndShoot>();
                if (guns != null)
                {
                    guns.AddAmmo(ammoRestored);
                    canGiveAmmo = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
    void Start()
    {
        canGiveAmmo = true;
    }

}
