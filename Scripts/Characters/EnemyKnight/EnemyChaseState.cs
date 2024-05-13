using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;
using System.Linq;

internal partial class EnemyChaseState : EnemyState
{

    private CharacterBody3D target;

    [Export]
    private Timer chaseTimer { get; set; }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);

        target = CharacterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        chaseTimer.Timeout += UpdateDestination;
        CharacterNode.AttackArea.BodyEntered += HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }


    protected override void ExitState()
    {
        base.ExitState();
        chaseTimer.Timeout -= UpdateDestination;
        CharacterNode.AttackArea.BodyEntered -= HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Move();
    }

    private void UpdateDestination()
    {
        destination = target.GlobalPosition;
        CharacterNode.NavigationAgent.TargetPosition = destination;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<AttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<ReturnState>();
    }

}
