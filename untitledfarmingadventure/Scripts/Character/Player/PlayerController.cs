using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerController : CharacterBody2D, IPlayerControlsListener
{
	[Export] private PlayerInputs _playerInputs;
	
	#region Required Nodes
	[Export] public RayCast2D CollisionDetection { get; private set; }
	[Export] public AnimationPlayer AnimationPlayer { get; private set; }
	[Export] public AnimationTree AnimationTree { get; set; }
	[Export] public Area2D HitBox { get; set; }
	[Export] public Sprite2D Sprite { get; private set; }
	#endregion

	private Vector2 _inputDir;
	private Vector2 _velocity;
	private Vector2 _lastValidInput = Vector2.Zero;
	private bool _isUsingTool = false;
	
	[Export] float _moveSpeed = 300f;
	private bool _isMoving = false;

	[Export] private int _tileSize = 32;

	private AnimationNodeStateMachinePlayback StateMachinePlayback;

	private double playBackPos;
	private double aniamtionLength;

	public override void _Ready()
	{
		RegisterListeners();
		AnimationTree.Active = true;
		AnimationTree.Set("parameters/IdleTree/blend_position", Vector2.Down);
		StateMachinePlayback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
	}

	public override void _ExitTree()
	{
		DeregisterListeners();
	}

	// Changes the player input to match the InputManager event firing.
	public void Move(Vector2 movement)
	{
		_inputDir = movement;
		
		if (_isMoving) { return;}
		
		_inputDir = new(Mathf.Sign(movement.X), Mathf.Sign(movement.Y));

		if (_inputDir != Vector2.Zero)
		{
			_lastValidInput = _inputDir;
		}
	}

	// Handles Interaction Inputs, which so far does nothing.
	public void Interact() 
	{
		GD.Print("Interacting.");
	}

	// Handles the attack input.
	public void Attack() 
	{
		_isAttacking = true;
		
		if (AnimationTree != null)
		{
			AnimationTree.Set("parameters/AttackTree/blend_position", _lastValidInput);
		}

		if (StateMachinePlayback.GetCurrentNode() != "AttackTree")
		{
			StateMachinePlayback.Travel("AttackTree");
		}
	}

	/*
		Moves the player in a grid based on tile size and player input direction.
		If the player is next to something on the last input, player can no longer move in that direction.
	*/
	async void MoveToNextTile()
	{
		CollisionDetection.TargetPosition = _inputDir * _tileSize;
		CollisionDetection.ForceRaycastUpdate();

		if (CollisionDetection.IsColliding())
		{
			if (CollisionDetection.GetCollider() is not PlayerController)
			{
				return;
			}
		}
		
		_isMoving = true;
		int delayTime = 1;

		Vector2 startingPos = Position;
		Vector2 targetPos = startingPos + _inputDir * _tileSize;

		while (Position.DistanceTo(targetPos) > 1f)
		{
			Position = Position.MoveToward(targetPos, _moveSpeed * (float)GetProcessDeltaTime());
			await Task.Delay(delayTime);
		}

		Position = targetPos;
		_isMoving = false;
	}

	// Registers the player inputs based on th eInputManager and PlayerInputs Resource
	void RegisterListeners()
	{
		_playerInputs.Movement += Move;
		_playerInputs.Interact += Interact;
		_playerInputs.Attack += Attack;
	}

	// Deregisters the events from RegisterListeners
	void DeregisterListeners()
	{
		_playerInputs.Movement -= Move;
		_playerInputs.Interact -= Interact;
		_playerInputs.Attack -= Attack;
	}

	void UpdateLookDir()
	{
		if (_lastValidInput == Vector2.Zero) { return; }

		if (_lastValidInput.X < 0f)
		{
			Sprite.FlipH = true;
		}
		else if (_lastValidInput.X > 0f)
		{
			Sprite.FlipH = false;
		}

		if (AnimationTree != null)
		{
			AnimationTree.Set("parameters/RunTree/blend_position", _lastValidInput);
			AnimationTree.Set("parameters/IdleTree/blend_position", _lastValidInput);
		}
	}

	void UpdateAnimationState()
	{
		if (_isAttacking) { return; }
		
		if (_inputDir == Vector2.Zero && !_isAttacking)
		{
			StateMachinePlayback.Travel("IdleTree");
		}
		else
		{
			StateMachinePlayback.Travel("RunTree");
		}
	}
}
