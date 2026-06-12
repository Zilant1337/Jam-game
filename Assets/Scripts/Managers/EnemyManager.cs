using System.Collections.Generic;
using System.Linq;
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
        Rifleman,
        Sniper,
        Boss
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
    private int enemyCount;

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

    public EnemySpawnerArea CurrentSpawnerArea { get => currentSpawnerArea; set => OnSpawnerAreaChange(value); }

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
            { EnemyType.Dummy,100},{ EnemyType.Chaser,100},{ EnemyType.Shotgunner,200},{ EnemyType.Rifleman,150}, { EnemyType.Boss,2000}, { EnemyType.Sniper,300}

        };
        enemyCount = 0;
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
        if(enemyCount<maxEnemiesOnField)
        {
            if (currentSpawnerArea.Spawn())
            {
                enemyCount++;                
            }
        }
    }
    private void OnSpawnerAreaChange(EnemySpawnerArea newEnemySpawnerArea)
    {
        currentSpawnerArea = newEnemySpawnerArea;
        List<GameObject> enemyObjects = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        List<EnemyScript> enemiesToDestroy = new List<EnemyScript>();
        foreach (GameObject gameObject in enemyObjects)
        {
            EnemyScript enemyScript = gameObject.GetComponent<EnemyScript>();
            if (enemyScript && !gameObject.GetComponent<BossScript>())
            {
                if (!CameraManager.Instance.CheckObjectVisibility(enemyScript.gameObject) && enemyScript.EnemySpawnerArea != currentSpawnerArea)
                    enemiesToDestroy.Add(enemyScript);
            }
        }
        for(int i = enemiesToDestroy.Count() - 1; i >= 0; i--)
        {
            Destroy(enemiesToDestroy[i].gameObject);
            enemyCount--;
        }
    }
    private void OnEnemyDeath(EnemyType enemyType, Transform transform)
    {
        if(enemyCount!=0)
            enemyCount--;
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
