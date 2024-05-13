using DungeonRPG.Scripts.Abilities;
using Godot;
using System;

public partial class Lightning : AbilityBase
{
    public override void _Ready()
    {
        base._Ready();
        playerNode.AnimationFinished += PlayerNode_AnimationFinished;
    }

    private void PlayerNode_AnimationFinished(StringName animName)
    {
        QueueFree();
    }
}
