using DungeonRPG.Scripts.General;
using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] 
    private Node Target;
    [Export]
    private Vector3 PositionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnGameEnd += HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        Reparent(GetTree().CurrentScene);
    }

    private void HandleStartGame()
    {
        Reparent(Target);

        Position = PositionFromTarget;
    }
}
