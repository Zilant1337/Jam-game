using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public enum EnemyType
    {
        Dummy
    }

    [SerializeField]
    private Transform enemyParentTransform;
    [SerializeField]
    private Transform enemySpawnersTransform;
    private List<EnemySpawner> enemySpawners;
    [SerializeField]
    private int maxEnemiesOnField;
    private int enemiesOnField;

    private Dictionary<EnemyType, int> enemyMonetaryValues;
    public UnityEvent <EnemyType,Transform> onEnemyDeath;
    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("Can't have more than one EnemyManager");
            Destroy(this);
            return;
        }
        instance = this;

        onEnemyDeath = new UnityEvent<EnemyType, Transform>();
        onEnemyDeath.AddListener(OnEnemyDeath);

        enemyMonetaryValues = new Dictionary<EnemyType, int>
        {
            { EnemyType.Dummy,100}

        };
        enemiesOnField = 0;
        enemySpawners = new List<EnemySpawner>();
    }
    private void Start()
    {
        foreach(Transform child in enemySpawnersTransform)
        {
            if (child.GetComponent<EnemySpawner>() != null)
                enemySpawners.Add(child.GetComponent<EnemySpawner>());
        }
    }
    private void Update()
    {
        foreach(EnemySpawner enemySpawner in enemySpawners)
        {
            if(enemiesOnField<maxEnemiesOnField){
                
                if (enemySpawner.Spawn())
                {
                    enemiesOnField++;
                    Debug.Log($"Spawned a new enemy, new enemy count: {enemiesOnField}");
                }
            }
        }
    }

    private void OnEnemyDeath(EnemyType enemyType, Transform transform)
    {
        if(enemiesOnField!=0)
            enemiesOnField--;
        Debug.Log($"Killed {transform.gameObject.name}, enemy count: {enemiesOnField}");
        MoneyAndPurchasing.instance.AddMoney(enemyMonetaryValues[enemyType]);
    } 
}
