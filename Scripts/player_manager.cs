using Godot;
using System;

//Player Manager
/*
* Player Manager is designed to be similar to a container
* It holds all the player values that will be kept track of
* The player_manager script will be the only player script attached
* The rest will be used as libraries and called from manager
*/

public partial class player_manager : CharacterBody2D
{
	// Player scripts
	player_movement player_movement_script;

	// Movement

	[Export]
	private float speed;
	[Export]
	private float gravity;
	[Export]
	private float jumpForce;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player_movement_script = new player_movement();
		player_movement_script._Ready(this, speed, gravity, jumpForce);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		player_movement_script._Process(delta);
	}

    public override void _PhysicsProcess(double delta)
    {
        player_movement_script._PhysicsProcess(delta);
    }

    public override void _Draw()
    {
        base._Draw();
		//DrawLine(Vector2.Zero, Vector2.Down * 1000, Godot.Color.Color8(255, 0, 0, 255), 1, false);
    }
}

#region Example Code
//Sets the velocity of the body and then tells it to move

/*
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
*/
#endregion
