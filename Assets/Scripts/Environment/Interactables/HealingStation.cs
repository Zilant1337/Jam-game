using UnityEngine;

public class HealingStation: PaidInteractable
{
    [SerializeField]
    Transform healthPackPositionTransform;
    [SerializeField]
    Transform healthPackPrefabTransform;
    [SerializeField]
    Transform healthPackTransform;
    [SerializeField]
    float healAmount;
    [SerializeField]
    float healRespawnTime;
    float healRespawnTimer;
    bool healActive;
    private void Start()
    {
        healRespawnTimer = 0;
        healthPackTransform = Instantiate(healthPackPrefabTransform, healthPackPositionTransform.position, healthPackPositionTransform.rotation, transform);
        paidInteractableUI.UpdatePaidInteractableText($"Heal {healAmount}:", price.ToString());
        healActive = true;
    }
    private void Update()
    {
        if (!healActive)
        {
            healRespawnTimer += Time.deltaTime;
            if (healRespawnTimer >= healRespawnTime)
            {
                healRespawnTimer = 0;
                healthPackTransform = Instantiate(healthPackPrefabTransform, healthPackPositionTransform.position,healthPackPositionTransform.rotation, transform);
                healActive = true;
            }
        }
    }
    protected override bool CheckReadiness()
    {
        return healActive;
    }
    protected override void PaidAction()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().AddHealth(healAmount);
        Destroy(healthPackTransform.gameObject);
        healActive=false;
    }
}
