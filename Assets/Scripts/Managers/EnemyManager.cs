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
    private Dictionary<EnemyType, int> enemyMonetaryValues;
    public UnityEvent <EnemyType> onEnemyDeath;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Can't have more than one EnemyManager");
            Destroy(this);
            return;
        }
        instance = this;
        onEnemyDeath = new UnityEvent<EnemyType>();
        onEnemyDeath.AddListener(OnEnemyDeath);
        enemyMonetaryValues = new Dictionary<EnemyType, int>
        {
            { EnemyType.Dummy,100}

        };
    }

    private void OnEnemyDeath(EnemyType enemyType)
    {
        Debug.Log($"Player killed {enemyType} and should get {enemyMonetaryValues[enemyType]}");
        MoneyAndPurchasing.instance.AddMoney(enemyMonetaryValues[enemyType]);
    } 
}
