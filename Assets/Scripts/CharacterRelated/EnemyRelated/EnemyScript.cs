using UnityEngine;

public class EnemyScript : CharacterScript

{
    [SerializeField]
    private EnemyManager.EnemyType enemyType;

    public EnemyManager.EnemyType EnemyType { get => enemyType; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
