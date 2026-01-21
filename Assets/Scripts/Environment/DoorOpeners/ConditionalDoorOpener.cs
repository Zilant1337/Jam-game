using System;
using System.Linq;
using UnityEngine;

public class ConditionalDoorOpener : MonoBehaviour
{
    [SerializeField]
    Door door;
    [SerializeField]
    DoorOpenerHelper[] openerHelpers;
    [SerializeField]
    bool[] openingProgress;
    private void Start()
    {
        // Создание флагов для проверки активации переключателей
        openingProgress = new bool[openerHelpers.Length];
    }
    public void AddHelper(DoorOpenerHelper newHelper)
    {
        openerHelpers.Append(newHelper);
        openingProgress.Append(false);
    }
    public void ProgressOpening(DoorOpenerHelper openerHelper)
    {
        // Если открыватель, который открывает дверь, есть в списке - ставим или снимаем флаг
        int openerIndex = Array.FindIndex(openerHelpers, opener => openerHelper==opener);
        if (openerIndex != -1)
        { 
            openingProgress[openerIndex] = !openingProgress[openerIndex];
        }
        // Если все флаги активированы, открываем дверь
        if (!Array.Exists(openingProgress, p => p == false))
        {
            door.Open();
        }
    }
    protected bool CheckReadiness()
    {
        return !door.IsMoving;
    }
}
