using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;
using System.Linq;

internal partial class AttackState : EnemyState
{
    private Vector3 TargetPosition;

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished += AnimationPlayerNode_AnimationFinished;
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);
        
        var target = CharacterNode.AttackArea
            .GetOverlappingBodies()
            .First();

        TargetPosition = target.GlobalPosition;
    }

    private void AnimationPlayerNode_AnimationFinished(StringName animName)
    {
        CharacterNode.ToggleHitBox(true);


        //Still in range
        var target = CharacterNode.AttackArea.GetOverlappingBodies() .FirstOrDefault();
        
        if(target is not null)
        {
            TargetPosition = target.GlobalPosition;
            var direction = CharacterNode
                .GlobalPosition
                .DirectionTo(TargetPosition);

            CharacterNode.Sprite3DNode.FlipH = direction.X < 0;

            CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);
        }
        else        //Not in range
        {
            var chaseTarget = CharacterNode
                .ChaseAreaNode
                .GetOverlappingBodies()
                .FirstOrDefault();

            if(chaseTarget is null)
            {
                CharacterNode.StateMachineNode.SwitchState<ReturnState>();
            }
            else
            {
                CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            }

            return;
        }
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= AnimationPlayerNode_AnimationFinished;
    }

    public void PerformHit()
    {
        CharacterNode.SetHitBoxPosition(TargetPosition);
        CharacterNode.ToggleHitBox(false);
    }


}
