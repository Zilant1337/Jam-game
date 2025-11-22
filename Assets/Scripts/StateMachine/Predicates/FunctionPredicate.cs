using System;
using UnityEngine;

public class FunctionPredicate : IPredicate
{
    protected Func<bool> function;
    public FunctionPredicate(Func<bool> function)
    {
        this.function = function;
    }
    public bool Evaluate() => function.Invoke();
}
