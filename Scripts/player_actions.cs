using Godot;
using System;

public class player_actions : Node
{
    // For now player actions are just different forms of attacks
    enum PlayerActions{
        NONE = 0,
        BASE_ACTION = 1
    }
    
    PlayerActions currentAction;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentAction = PlayerActions.NONE;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("player_action")){
            currentAction = PlayerActions.BASE_ACTION;
        }
    }

    public void ExecutePlayerActions(){
        switch(currentAction){
            case PlayerActions.NONE:
                GD.Print("No Action");
                break;
            case PlayerActions.BASE_ACTION:
                GD.Print("Base Action");
                break;
            default:
                GD.PushWarning("Unknown Action for execution");
                break;
        }

        currentAction = PlayerActions.NONE;
    }
}
