using Godot;
using System;
using System.Diagnostics;

public class player_movement : Node
{
    Vector2 velocity = Vector2.Zero;
	float speed = 10;
	float jumpForce = 10;
	float gravity;

	RayCast2D raycast;

	player_manager playerManager;

    public void Initialise(player_manager pManager, float horizontalSpeed, float gravityAcceleration, float jump)
	{
		playerManager = pManager;

		speed = horizontalSpeed;

		gravity = gravityAcceleration;
		jumpForce = jump;

		raycast = pManager.GetNode<RayCast2D>("FloorCast");
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public override void _PhysicsProcess(float delta)
    {
		// Calculates movement
		VerticalMovement();
		HorizontalMovement();

		// Executes movement
		MovePlayer();
    }

	private void VerticalMovement(){
		if(IsGrounded()){
			velocity.y = 0;
			//GD.Print("Grounded");
			if(Input.IsActionJustPressed("jump")){
				velocity.y = -jumpForce;
			}
			return;
		}
		ApplyGravity();
		// Temp Jump Cut
		if(Input.IsActionJustReleased("jump") && velocity.y < 0){
			//GD.Print(velocity.y);
			velocity.y = 0;
		}
	}

    private void HorizontalMovement(){
		// Input
		float horizontalMovementInput = 0;
		if(Input.IsActionPressed("move_right")){
			horizontalMovementInput += 1;
			playerManager.ChangeFacing(true);
		}
		if(Input.IsActionPressed("move_left")){
			horizontalMovementInput -= 1;
			playerManager.ChangeFacing(false);
		}

        //GD.Print("Movement: " + horizontalMovementInput);

		// Movement
		float targetSpeed = horizontalMovementInput * speed;

		float speedDifference = targetSpeed - velocity.x;

		velocity.x += speedDifference;

        //GD.Print(speedDifference);
	}

	// Helps wrap my head around movement
	private void MovePlayer(){
		playerManager.MoveAndCollide(velocity);

		// Animation update
        if(velocity == Vector2.Zero){
			playerManager.ChangeState(PlayerState.IDLE);
			return;
		}
		playerManager.ChangeState(PlayerState.MOVING);
	}

	private void ApplyGravity(){
		if(!IsGrounded()){
			velocity.y += gravity;
            return;
		}
        velocity.y = 0;
	}

	// Since horizontal movement is handled with MoveAndCollide, IsOnFloor will never be updated
	// IsOnFloor is updated within MoveAndSlide which also calls MoveAndCollide
	// As such I'm using a custom ground check 
	private bool IsGrounded(){
		return raycast.IsColliding();
	}
}
