using Godot;
using System;
using System.IO;

public struct BeatMapInfo{
        public float startTime;
        public string songPath;
        public float[] spawnTimes;
    }

public class beats_system_manager : Node
{

    float stageTimer;

    // Debug Testing
    [Export]
    float[] debugSpawnTimes;
    [Export]
    string debugSongPath;

    private BeatMapInfo currentBeatMapInfo;

    private int spawnBeatIndex;

    private int beatsToSpawn;

    AudioStreamPlayer musicPlayer;

    player_manager player_manager_script;

    public void LoadBeatMap(BeatMapInfo newBeatMap){
        currentBeatMapInfo = newBeatMap;

        stageTimer = -currentBeatMapInfo.startTime;

        spawnBeatIndex = 0;
        beatsToSpawn = currentBeatMapInfo.spawnTimes.Length;

        musicPlayer.Stream = GD.Load<AudioStream>(currentBeatMapInfo.songPath);
        musicPlayer.Playing = true;
        

        player_manager_script = GetNode<KinematicBody2D>("/root/Stage/Player") as player_manager;
        
        player_manager_script.TestFunction();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Debug testing
        BeatMapInfo debugBeatMapInfo;
        debugBeatMapInfo.startTime = 0;
        debugBeatMapInfo.songPath = debugSongPath;
        debugBeatMapInfo.spawnTimes = debugSpawnTimes;

        musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");

        

        LoadBeatMap(debugBeatMapInfo);


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        stageTimer += delta;

        // This is bad for several reasons
        /*
            It spawns it once the time to spawn it has passed and doesn't correct itself so beats will finish at different times it has been mapped to
            Loading an object and instantiating takes time and is costly
                We know how many beats would possibly be on screen
                As such creating an object pool that loads all the objects needed is important
        */
        if(spawnBeatIndex != beatsToSpawn){
            if(currentBeatMapInfo.spawnTimes[spawnBeatIndex] <= stageTimer){
                //GD.Print("Spawn");
                spawnBeatIndex++;

                var beatTemplate = GD.Load<PackedScene>("res://Scenes/beat.tscn");
                var beatInstance = beatTemplate.Instance();
                this.AddChild(beatInstance);
            }
        }
    }

    public void ExecutePlayerActions(){
        player_manager_script.ExecutePlayerActions();
    }


}
