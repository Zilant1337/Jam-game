using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public enum EnemyType
    {
        Dummy,
        Chaser,
        Shotgunner,
        Rifleman
    }
    
    [SerializeField]
    private Transform enemyParentTransform;
    [SerializeField]
    private Transform enemySpawnerAreasTransform;
    private List<EnemySpawnerArea> enemySpawnerAreas;
    [SerializeField]
    private EnemySpawnerArea initialSpawnerArea;
    private EnemySpawnerArea currentSpawnerArea;


    [SerializeField]
    private int maxEnemiesOnField;
    private int enemiesOnField;

    [SerializeField]
    private float healSpawnChance;
    [SerializeField]
    private float ammoSpawnChance;

    [SerializeField]
    private Transform healthPackPrefabTransform;
    [SerializeField]
    private Transform ammoPackPrefabTransform;

    System.Random rand;

    private Dictionary<EnemyType, int> enemyMonetaryValues;
    public UnityEvent <EnemyType,Transform> onEnemyDeath;

    public EnemySpawnerArea CurrentSpawnerArea { get => currentSpawnerArea; set => currentSpawnerArea = value; }

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
            { EnemyType.Dummy,100},{ EnemyType.Chaser,100},{ EnemyType.Shotgunner,200},{ EnemyType.Rifleman,150}

        };
        enemiesOnField = 0;
        enemySpawnerAreas = new List<EnemySpawnerArea>();
    }
    private void Start()
    {
        rand = new System.Random();
        foreach (Transform child in enemySpawnerAreasTransform)
        {
            if (child.GetComponent<EnemySpawner>() != null)
                enemySpawnerAreas.Add(child.GetComponent<EnemySpawnerArea>());
        }
        currentSpawnerArea = initialSpawnerArea;
    }
    private void Update()
    {
        if(enemiesOnField<maxEnemiesOnField)
        {
            if (currentSpawnerArea.Spawn())
            {
                enemiesOnField++;
                Debug.Log($"Spawned a new enemy, new enemy count: {enemiesOnField}");
            }
        }
    }

    private void OnEnemyDeath(EnemyType enemyType, Transform transform)
    {
        if(enemiesOnField!=0)
            enemiesOnField--;
        Debug.Log($"Killed {transform.gameObject.name}, enemy count: {enemiesOnField}");
        int ammoOrHealth = rand.Next(0,2);
        switch (ammoOrHealth)
        {
            case 0:
                float healthRand = Random.Range(0,100);
                if (healthRand < 1 / healSpawnChance)
                {
                    Instantiate(healthPackPrefabTransform,transform.position,Quaternion.identity);
                }
            break;
            case 1:
                float ammoRand = Random.Range(0, 100);
                if (ammoRand < 1 / ammoSpawnChance)
                {
                    Instantiate(ammoPackPrefabTransform, transform.position, Quaternion.identity);
                }
                break;
        }

        MoneyAndPurchasing.instance.AddMoney(enemyMonetaryValues[enemyType]);
    } 
}
