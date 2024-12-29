using Godot;
using System;

public partial class FollowCamera : Camera2D
{
    [Export] public float SmoothSpeed { get; private set; } = 5f;

    private Player _playerFollow;

    public override void _Ready()
    {
        GameStateManager.Instance.CurrentGameState += SetActiveCamera;

        var localPlayers = GetTree().GetNodesInGroup("Player");

        if (localPlayers.Count > 0)
        {
            foreach (var player in localPlayers)
            {
                var playerNode = player as Player;

                if (playerNode != null && !HasCameraAttached(playerNode))
                {
                    _playerFollow = playerNode;
                    break;
                }
            }
        }

        if (_playerFollow == null)
        {
            GD.Print("No viable Player found...");
        }
    }

    public override void _ExitTree()
    {
        GameStateManager.Instance.CurrentGameState -= SetActiveCamera;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_playerFollow == null) { return; }

        Vector2 currentPos = GlobalPosition;

        Vector2 targetPos = _playerFollow.GlobalPosition;
        
        Vector2 newPos = currentPos.Lerp(targetPos, SmoothSpeed * (float)delta);

        GlobalPosition = newPos;
    }

    bool HasCameraAttached(Player player)
    {
        return player.GetNodeOrNull<Camera2D>("FollowCamera") != null;
    }

    void SetActiveCamera(GameState state)
    {
        this.Enabled = state != GameState.CutScene;
    }
}
