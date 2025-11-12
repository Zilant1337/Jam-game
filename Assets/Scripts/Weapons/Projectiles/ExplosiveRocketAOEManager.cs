using UnityEngine;

public class ExplosiveRocketAOEManager : MonoBehaviour
{
    [SerializeField]
    ExplosiveRocket explosiveRocket;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            explosiveRocket.AddHealthToDamage(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            explosiveRocket.RemoveHealthToDamage(health);
        }

    }
}
