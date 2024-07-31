using Godot;
using System;

public enum PlayerState{
        IDLE = 0,
        MOVING = 1
}

public class player_manager : KinematicBody2D
{

    // Player scripts
	player_movement player_movement_script;
    player_actions player_actions_script;
    player_animation player_animation_script;

	// Movement
	[Export]
	private float speed;
	[Export]
	private float gravity;
	[Export]
	private float jumpForce;
    private bool isFacingRight = true;

    private PlayerState currentPlayerState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentPlayerState = PlayerState.IDLE;

        player_movement_script = new player_movement();
		player_movement_script.Initialise(this, speed, gravity, jumpForce);
        player_animation_script = new player_animation();
        player_animation_script.Initialise(this);
        player_actions_script = new player_actions();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        player_movement_script._Process(delta);
        player_actions_script._Process(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        player_movement_script._PhysicsProcess(delta);
    }

    public void ChangeState(PlayerState newState){
        switch(newState){
            case PlayerState.IDLE:
            case PlayerState.MOVING:
                currentPlayerState = newState;
                player_animation_script.UpdateAnimationState(currentPlayerState);
                break;
            default:
                GD.PushError("Unknown player state");
                break;
        }
        
    }

    public void ChangeFacing(bool newFacingDirection){
        if(isFacingRight == newFacingDirection){
            return;
        }

        isFacingRight = newFacingDirection;
        player_animation_script.UpdateAnimationDirection(isFacingRight);
    }

    private void InputUpdate(){
        
    }

    public void ExecutePlayerActions(){
        player_actions_script.ExecutePlayerActions();
    }

    public void TestFunction(){
        GD.Print("Player Manager Here");
    }
}
