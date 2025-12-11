using UnityEngine;

public class ExplosiveRocketAOEManager : MonoBehaviour
{
    [SerializeField]
    protected ExplosiveRocket explosiveRocket;
    protected void OnTriggerEnter(Collider other)
    {
        explosiveRocket.OnCollision(other);
    }
}
