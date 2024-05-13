using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class StunState : EnemyState
{
    protected override void EnterState()
    {
        //base.EnterState();
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_STUN);
        CharacterNode.AnimationPlayerNode.AnimationFinished += AnimationPlayerNode_AnimationFinished;

    }

    private void AnimationPlayerNode_AnimationFinished(StringName animName)
    {
        if(CharacterNode.AttackArea.HasOverlappingBodies())
        {
            CharacterNode.StateMachineNode.SwitchState<AttackState>();
        }
        else if (CharacterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
        else
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
     
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= AnimationPlayerNode_AnimationFinished;
    }
}
