using Godot;
using System;

public partial class GameStateManager : Singleton<GameStateManager>
{
    public event Action<GameState> CurrentGameState;

    GameState _currentState;

    public GameState GameState 
    {
        get => _currentState;
        set
        {
            if (_currentState != value)
            {
                _currentState = value;
                CurrentGameState?.Invoke(_currentState);
            }
        }
    }

    protected override void Initialize()
    {
        _currentState = GameState.GamePlay;
    }
}

public enum GameState { Menu, GamePlay, Inventory, Dialogue, CutScene }
