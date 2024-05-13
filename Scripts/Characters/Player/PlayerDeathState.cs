using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DEATH);
        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        GameEvents.RaiseGameEnd();
        CharacterNode.QueueFree();
    }
}
