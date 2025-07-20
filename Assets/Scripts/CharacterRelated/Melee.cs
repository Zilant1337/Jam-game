using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    protected float meleeDistance;
    [SerializeField]
    protected float meleeDamage;
    [SerializeField]
    // In seconds
    protected float meleeStaminaCost;
    protected float meleeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meleeTime = 0;
    }

    void Update()
    {
        if (meleeTime != 0)
        {
            meleeTime -= Time.deltaTime;
            if (meleeTime < 0)
            {
                meleeTime = 0;
            }
        }
    }
}
