using Godot;
using System;
using System.Drawing;

public partial class player_movement : Node2D
{
	Vector2 velocity = Vector2.Zero;
	float speed = 10;
	float jumpForce = 10;
	float gravity;

	RayCast2D raycast;


	CharacterBody2D playerBody;
	// Called when the node enters the scene tree for the first time.
	public void _Ready(CharacterBody2D pBody, float horizontalSpeed, float gravityAcceleration, float jump)
	{
		playerBody = pBody;

		speed = horizontalSpeed;

		gravity = gravityAcceleration;
		jumpForce = jump;

		raycast = new RayCast2D();
		raycast.Position = Vector2.Zero;
		raycast.TargetPosition = Vector2.Down;
		playerBody.AddChild(raycast);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void _PhysicsProcess(double delta)
    {
		// Calculates movement
		VerticalMovement();
		HorizontalMovement();

		// Executes movement
		MovePlayer();
    }

	private void VerticalMovement(){
		if(IsGrounded()){
			velocity.Y = 0;
			//GD.Print("Grounded");
			if(Input.IsActionJustPressed("jump")){
				velocity.Y = -jumpForce;
			}
			return;
		}
		ApplyGravity();
		// Temp Jump Cut
		if(Input.IsActionJustReleased("jump")){
			velocity.Y = 0;
		}
	}

    private void HorizontalMovement(){
		// Input
		float horizontalMovementInput = 0;
		if(Input.IsActionPressed("move_right")){
			horizontalMovementInput += 1;
		}
		if(Input.IsActionPressed("move_left")){
			horizontalMovementInput -= 1;
		}

		// Movement
		float targetSpeed = horizontalMovementInput * speed;

		float speedDifference = targetSpeed - velocity.X;

		velocity.X += speedDifference;
	}

	// Helps wrap my head around movement
	private void MovePlayer(){
		playerBody.MoveAndCollide(velocity);
	}

	private void ApplyGravity(){
		if(!IsGrounded()){
			velocity += new Vector2(0.0f, gravity);
		}

	}

	// Since horizontal movement is handled with MoveAndCollide, IsOnFloor will never be updated
	// IsOnFloor is updated within MoveAndSlide which also calls MoveAndCollide
	// As such I'm using a custom ground check 
	private bool IsGrounded(){
		return raycast.IsColliding();
	}
}
