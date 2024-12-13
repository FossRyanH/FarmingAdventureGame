using Godot;
using System;

public partial class GameStateManager : Singleton<GameStateManager>
{
    public GameState GameState { get; set; }

    protected override void Initialize()
    {
        GameState = GameState.GamePlay;
    }
}

public enum GameState { Menu, GamePlay, Inventory, Dialogue }
