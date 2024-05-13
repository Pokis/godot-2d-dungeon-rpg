using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        GD.Print("Enemy death state entered.");
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DEATH);
        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        //GetParent().QueueFree();
        CharacterNode.PathNode.QueueFree();
    }
}
