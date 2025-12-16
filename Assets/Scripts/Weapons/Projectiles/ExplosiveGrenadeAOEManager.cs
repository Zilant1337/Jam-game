using UnityEngine;

public class ExplosiveGrenadeAOEManager : MonoBehaviour
{
    [SerializeField]
    protected Grenade grenade;
    protected  void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            grenade.AddHealthToDamage(health);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            grenade.RemoveHealthToDamage(health);
        }

    }
}
