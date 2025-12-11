using UnityEngine;

public class ExplosiveGrenadeAOEManager : MonoBehaviour
{
    [SerializeField]
    protected Grenade grenade;
    protected void OnCollisionEnter(Collision collision)
    {
        grenade.OnCollision(collision);
    }
}
