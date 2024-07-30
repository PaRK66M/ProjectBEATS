using Godot;
using System;

public class player_animation : Node
{
    private PlayerState currentAnimationState;
    private bool isFacingRight;

    private AnimatedSprite playerAnimator;
    player_manager playerManager;


    public void Initialise(player_manager pManager)
	{
		playerManager = pManager;

        playerAnimator = pManager.GetNode<AnimatedSprite>("PlayerAnimator");
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void UpdateAnimationState(PlayerState newPlayerState){

        currentAnimationState = newPlayerState;

       // GD.Print(newPlayerState);

        switch(newPlayerState){
            case PlayerState.IDLE:
                playerAnimator.Animation = "idle";
                break;
            case PlayerState.MOVING:
                playerAnimator.Animation = "walk";
                break;
            default:
                break;
        }

    }

    public void UpdateAnimationDirection(bool newFacingDirection){

        isFacingRight = newFacingDirection;
        playerAnimator.Scale = new Vector2(isFacingRight ? 1 : -1, 1);
    }
}
