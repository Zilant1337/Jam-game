using UnityEngine;

public class Transition : ITransition
{
    public Transition(IState targetState,IPredicate condition)
    {
        TargetState = targetState;
        Condition = condition; 
    }

    public IState TargetState { get;}
    public IPredicate Condition { get; }
}
