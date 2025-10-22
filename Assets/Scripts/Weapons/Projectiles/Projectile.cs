using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody projectileRigidbody;
    [SerializeField]
    protected float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    protected abstract void Move();
}
