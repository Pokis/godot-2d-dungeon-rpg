using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class PlayerAttackState : CharacterState
{

    private int comboCounter = 1;
    private int maxComboCount = 2;

    [Export]
    private PackedScene lightningScene;

    [Export]
    private Timer Timer { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Timer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK + comboCounter, customSpeed: 1.5f);
        CharacterNode.AnimationPlayerNode.AnimationFinished += AnimationPlayerNode_AnimationFinished;
        CharacterNode.HitBox.BodyEntered += HitBox_BodyEntered;
    }

    private void HitBox_BodyEntered(Node3D body)
    {
        if(comboCounter != maxComboCount)
        {
            return;
        }

        var lightning = lightningScene.Instantiate<Node3D>();
        lightning.Position = body.GlobalPosition;
        GetTree().CurrentScene.AddChild(lightning);
    }

    protected override void ExitState()
    {
        CharacterNode.HitBox.BodyEntered -= HitBox_BodyEntered;
        CharacterNode.AnimationPlayerNode.AnimationFinished -= AnimationPlayerNode_AnimationFinished;
        Timer.Start();
    }

    private void AnimationPlayerNode_AnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        CharacterNode.ToggleHitBox(true);
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPosition = CharacterNode.Sprite3DNode.FlipH 
            ? Vector3.Left 
            : Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        CharacterNode.HitBox.Position = newPosition;

        CharacterNode.ToggleHitBox(false);

        GD.Print("Perform hit!");
    }
}
