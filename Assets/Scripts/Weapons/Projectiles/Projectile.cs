using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody projectileRigidbody;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float timeToSelfDestruct;
    protected float selfDestructTimer;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    protected abstract void Move();
    protected virtual void ProgressSelfDestructTimer()
    {
        selfDestructTimer += Time.deltaTime;
        if (selfDestructTimer >= timeToSelfDestruct)
        {
            Destroy(gameObject);
            return;
        }
    }
}
