using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    StateNode current;
    Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransitions = new();
    public void Update()
    {
        // Если выполнено хоть одно условие для переключения состояния, переключаем
        var transition = GetTransition();
        if ( transition!=null)
        {
            ChangeState(transition.TargetState);
        }
        // Выполняем действие, нужное для состояния
        current.State?.Update();
    }
    public void FixedUpdate()
    {
        current.State?.FixedUpdate();
    }
    public void SetState(IState state)
    {
        current = nodes[state.GetType()];
        current.State?.OnEnter();
    }
    // Смена состояния с вызовом методов выхода и входа у старого и нового состояния соответственно
    private void ChangeState(IState state)
    {
        if (state == current.State) return;

        var previousState = current.State;
        var nextState = nodes[state.GetType()].State;

        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state.GetType()];
    }

    private ITransition GetTransition()
    {
        foreach(var transition in anyTransitions)
            if(transition.Condition.Evaluate())
                return transition;

        foreach(var transition in current.Transitions)
            if(transition.Condition.Evaluate()) 
                return transition;

        return null;
    }

    public void AddTransition(IState fromState, IState targetState, IPredicate condition)
    {
        GetOrAddNode(fromState).AddTransition(GetOrAddNode(targetState).State, condition);
    }
    public void AddAnyTransition(IState targetState, IPredicate condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(targetState).State,condition));
    }
    StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }

        return node;
    }
    // Класс, содержащий состояние и набор предикат для его перехода в другие состояния
    class StateNode
    {
        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public IState State { get; }
        public HashSet<ITransition> Transitions { get;}
        public void AddTransition(IState targetState,IPredicate condition)
        {
            Transitions.Add(new Transition(targetState,condition));
        }
    }
}
