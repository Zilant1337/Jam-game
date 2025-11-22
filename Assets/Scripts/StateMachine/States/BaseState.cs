using UnityEngine;

public abstract class BaseState : IState
{
    [SerializeField]
    protected EnemyScript enemyScript;
    protected BaseState(EnemyScript enemyScript)
    {
        this.enemyScript = enemyScript;
    }
    public virtual void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
        throw new System.NotImplementedException();
    }
    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}
