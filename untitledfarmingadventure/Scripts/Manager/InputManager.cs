using Godot;
using System;

public partial class InputManager : Singleton<InputManager>
{
	#region Input Resources
	[Export] private PlayerInputs _playerInputs;
	[Export] public MenuInputs MenuInputs { get; set; }
	#endregion
	
	// Vector for input
	private Vector2 _inputVector;

	public override void _Input(InputEvent @event)
	{
		if (GameStateManager.Instance.GameState == GameState.GamePlay)
		{
			_inputVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
			// Player Inputs
			if (_inputVector != Vector2.Zero)
			{
				_playerInputs.HandleMovement(_inputVector);
			}
			else
			{
				_playerInputs.HandleMovement(_inputVector);
			}

			if (@event.IsActionPressed("Interact"))
			{
				_playerInputs.HandleInteract();
			}
			// Opens Inventory
			if (@event.IsActionPressed("OpenInventory"))
			{
				MenuInputs.HandleOpenInventory();
			}
		}
		else if (GameStateManager.Instance.GameState == GameState.Inventory)
		{
			if (@event.IsActionPressed("CloseInventory"))
			{
				MenuInputs.HandleCloseInventory();
			}
		}
		
	}
}
