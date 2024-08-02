using Godot;
using System;

public class main_menu : Node
{
    private Button gameButton;
    private Button beatmapButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameButton = GetNode<Button>("GameButton");
        beatmapButton = GetNode<Button>("SongMakingButton");
            
    }

    public void LoadScene(string scenePath){
        GetTree().ChangeScene(scenePath);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(gameButton.Pressed){
            LoadScene("res://Scenes/game_scene.tscn");
        }
        if(beatmapButton.Pressed){
            LoadScene("res://Scenes/beatmap_creation.tscn");
        }
    }
}
