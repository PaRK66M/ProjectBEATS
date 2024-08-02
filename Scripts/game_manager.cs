using Godot;
using System;

public class game_manager : Node
{
    Button startGame;

    // Debug Testing
    [Export]
    float[] debugSpawnTimes;
    [Export]
    string debugSongPath;

    BeatMapInfo selectedBeatMap;

    PackedScene stage_scene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        startGame = GetNode<Button>("StartGameButton");

        stage_scene = GD.Load<PackedScene>("res://Scenes/basic_stage.tscn");
    }

    public void LoadGame(){
        var stage_instance = stage_scene.Instance();
        this.AddChild(stage_instance);

        var beats_manager_script = GetNode<Node>("/root/GameManager/Stage/BeatsManager") as beats_system_manager;
        beats_manager_script.LoadBeatMap(selectedBeatMap);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(startGame.Pressed){
            selectedBeatMap.spawnTimes = debugSpawnTimes;
            selectedBeatMap.songPath = debugSongPath;
            SetProcess(false);
            startGame.QueueFree();
            LoadGame();
        }
    }
}
