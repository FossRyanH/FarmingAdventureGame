using Godot;
using System;

public partial class StateMachine : CharacterBody2D
{
    public IState CurrentState { get; set; }
    public event Action<IState> StateChanged;

    // Changes the Current state into the next state in dependent on Inputs.
    public void ChangeState(IState nextState)
    {
        if (CurrentState == null) { return; }
        
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        
        StateChanged?.Invoke(nextState);
    }

    protected void InitState(IState firstState)
    {
        if (CurrentState != null) { return; }
        
        CurrentState = firstState;
        firstState.Enter();
        
        StateChanged?.Invoke(firstState);
    }

    public override void _Process(double delta)
    {
        CurrentState?.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsUpdate(delta);
    }
}
