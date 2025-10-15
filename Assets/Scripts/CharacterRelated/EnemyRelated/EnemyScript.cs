using UnityEngine;

public class EnemyScript : CharacterScript

{
    [SerializeField]
    private EnemyManager.EnemyType enemyType;

    public EnemyManager.EnemyType EnemyType { get => enemyType; }
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
