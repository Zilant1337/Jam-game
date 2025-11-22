using UnityEngine;

public interface ITransition
{
    IState TargetState { get; }
    IPredicate Condition { get; }
}
