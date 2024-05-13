using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class PlayerDashState : PlayerState
{
   
    [Export]
    private Timer DashDurationTimerNode;

    [Export]
    private Timer DashTimeoutTimerNode;

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float speed = 0.1f;

    [Export]
    private PackedScene bombScene;


    public override void _Ready()
    {
        base._Ready();
        DashDurationTimerNode.Timeout += DashTimerNode_Timeout;
        CanTransition = () => DashTimeoutTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    private void DashTimerNode_Timeout()
    {
        DashTimeoutTimerNode.Start();
        CharacterNode.Velocity = Vector3.Zero;
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DASH);
        CharacterNode.Velocity = new Vector3(CharacterNode.Velocity.X, 0, CharacterNode.Velocity.Y);

        if (CharacterNode.Velocity == Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.Sprite3DNode.FlipH
                ? Vector3.Left
                : Vector3.Right;
        }

        CharacterNode.Velocity *= speed;        

        DashDurationTimerNode.Start();

        var newBombScene = bombScene.Instantiate<Node3D>();
        
        GetTree().CurrentScene.AddChild(newBombScene);
        newBombScene.GlobalPosition = CharacterNode.GlobalPosition;
    }
}
