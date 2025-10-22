using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
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
