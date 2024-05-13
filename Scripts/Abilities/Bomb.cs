using DungeonRPG.Scripts.Abilities;
using DungeonRPG.Scripts.General;
using Godot;
using System;

public partial class Bomb : AbilityBase
{
    public override void _Ready()
    {
        base._Ready();
        playerNode.AnimationFinished += PlayerNode_HandleExpandAnimationFinished;
        playerNode.AnimationFinished += PlayerNode_HandleExplosionAnimationFinished;
    }

    public float GetDamage()
    {
        return Damage;
    }

    private void PlayerNode_HandleExplosionAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIMATION_EXPLOSION)
        {
            QueueFree();
        }
    }

    private void PlayerNode_HandleExpandAnimationFinished(StringName animName)
    {
        if(animName != GameConstants.ANIMATION_EXPAND)
        {
            return;
        }

        playerNode.Play(GameConstants.ANIMATION_EXPLOSION);
    }
}
