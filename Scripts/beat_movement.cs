using Godot;
using System;

public class beat_movement : Node
{
    private event Action BeatUpdateAction;

    [Export]
    private float travelDistance;
    [Export]
    private float endPoint;
    [Export]
    private float travelTime;

    [Export]
    private float beatHeight;
    [Export]
    private float beatSize;

    private float leftStartPoint;
    private float rightStartPoint;

    private float bothStartPointsAdded;

    private float actualEndPoint;

    private float travelTimer;
    private float travelProgress;

    private TextureRect leftBeat;
    private TextureRect rightBeat;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        leftBeat = GetNode<TextureRect>("LeftBeat");
        rightBeat = GetNode<TextureRect>("RightBeat");

        travelTimer = 0.0f;
        actualEndPoint = endPoint - beatSize / 2;
        leftStartPoint = actualEndPoint - travelDistance;
        rightStartPoint = actualEndPoint + travelDistance;

        bothStartPointsAdded = leftStartPoint + rightStartPoint;

        BeatUpdateAction += MoveBeat;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        travelTimer += delta;
        travelProgress = travelTimer / travelTime;
        
        BeatUpdateAction();
    }

    private void MoveBeat(){
        
        if(travelProgress < 1){
            float leftPosition = Mathf.Lerp(leftStartPoint, actualEndPoint, travelProgress);
            float rightPosition = bothStartPointsAdded - leftPosition;

            leftBeat.RectPosition = new Vector2(leftPosition, beatHeight);
            rightBeat.RectPosition = new Vector2(rightPosition, beatHeight);
            return;
        }

        GD.Print(actualEndPoint);

        leftBeat.RectPosition = new Vector2(actualEndPoint, beatHeight);
        rightBeat.RectPosition = new Vector2(actualEndPoint, beatHeight);

        BeatUpdateAction -= MoveBeat;
        BeatUpdateAction += ActivatedVisuals;
        
    }

    private void ActivatedVisuals(){
        //BeatUpdateAction -= ActivatedVisuals;
        DestroyBeat();
    }

    // To change
    /*
        Making object pool but can't hide as node doesn't contain visibility (only node2d/3d does)
        Next best solution, move the beats to the shadow realm
    */
    private void DestroyBeat(){
        //this.QueueFree();
    }
}
