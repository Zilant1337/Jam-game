using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionalDoorOpener : MonoBehaviour
{
    [SerializeField]
    protected Door door;
    [SerializeField]
    protected List<DoorOpenerHelper> openerHelpers;
    [SerializeField]
    protected List<bool> openingProgress;
    protected void Start()
    {
        // Создание флагов для проверки активации переключателей
        openingProgress = new List<bool>(new bool[openerHelpers.Count]);
    }
    public void AddHelper(DoorOpenerHelper newHelper)
    {
        openerHelpers.Add(newHelper);
        openingProgress.Add(false);
    }
    public void ProgressOpening(DoorOpenerHelper openerHelper, bool isSuccessful)
    {
        // Если открыватель, который открывает дверь, есть в списке - ставим или снимаем флаг
        int openerIndex = openerHelpers.IndexOf(openerHelper);
        if (openerIndex != -1)
        { 
            openingProgress[openerIndex] = !openingProgress[openerIndex];
        }
        // Если все флаги активированы, открываем дверь
        if (!openingProgress.Contains(false))
        {
            door.Open();
        }
    }
    protected bool CheckReadiness()
    {
        return !door.IsMoving;
    }
}
