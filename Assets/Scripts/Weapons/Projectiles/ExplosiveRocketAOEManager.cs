using UnityEngine;

public class ExplosiveRocketAOEManager : MonoBehaviour
{
    [SerializeField]
    ExplosiveRocket explosiveRocket;
    protected void OnTriggerEnter(Collider other)
    {
        explosiveRocket.OnCollision(other);
    }
}
