using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class EnemyIdleState : EnemyState
{

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        CharacterNode.StateMachineNode.SwitchState<ReturnState>();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
        CharacterNode.ChaseAreaNode.BodyShapeEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();
        CharacterNode.ChaseAreaNode.BodyShapeEntered -= HandleChaseAreaBodyEntered;
    }
}
