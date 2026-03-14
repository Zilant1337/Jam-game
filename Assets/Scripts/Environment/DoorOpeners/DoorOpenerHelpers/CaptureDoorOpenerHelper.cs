using UnityEngine;

public class CaptureDoorOpenerHelper : DoorOpenerHelper
{
    [SerializeField]
    float timeToActivate;
    [SerializeField]
    float deactivationMultiplier;
    [SerializeField]
    ProgressBar progressBar;

    float activationTimer;

    bool isProgressing;
    // Включил ли игрок объект
    bool isActive;
    // Простоял ли игрок нужное время
    bool isCaptured;

    private void Start()
    {
        isActive = false;
        isProgressing = false;
        isCaptured = false;
        activationTimer = 0;
        progressBar.gameObject.SetActive(false);
    }
    // Если игрок входит в область захвата, начинаем прогресс
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (!isCaptured && other.gameObject.CompareTag("Player"))
        {
            isProgressing = true;
        }
    }
    // Если игрок выходит из области, приостанавливаем прогресс
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (!isCaptured && other.gameObject.CompareTag("Player"))
        {
            isProgressing = false;
        }
    }
    private void Update()
    {
        // Если игрок активировал переключатель, стоит в области и зона ещё не закончила прогресс, считаем время
        if (isProgressing && isActive && !isCaptured)
        {
            activationTimer += Time.deltaTime;
            // Если игрок простоял нужное количество времени в зоне, говорим открывателю двери о том, что мы закончили и делаем объект неактивным
            if (activationTimer >= timeToActivate)
            {
                activationTimer = timeToActivate;
                isCaptured = true;
                isProgressing = false;
                isActive = false;
                progressBar.gameObject.SetActive(false);
                ProgressDoorOpener();
            }
            if(!isCaptured)
                progressBar.UpdateProgressBar(activationTimer/timeToActivate);
        }
        // Если объект активирован, но игрок вышел за пределы области, начинаем отсчитывать назад с указанным модификатором и выключаем объект если таймер достиг 0
        if(!isProgressing && isActive && !isCaptured && activationTimer>0)
        {
            activationTimer -= Time.deltaTime * deactivationMultiplier;
            if (activationTimer <= 0)
            {
                activationTimer = 0;
                isActive = false;
                progressBar.gameObject.SetActive(false);
            }
            progressBar.UpdateProgressBar(activationTimer / timeToActivate);
        }
    }
    public override void InteractAction(Collider other)
    {
        if (!isActive)
        {
            isActive = true;
            progressBar.gameObject.SetActive(true);
            progressBar.UpdateProgressBar(activationTimer / timeToActivate);
        }
    }
}
