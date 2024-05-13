using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class ReturnState : EnemyState
{

    public override void _Ready()
    {
        base._Ready();
        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if(CharacterNode.NavigationAgent.IsNavigationFinished())
        {
            CharacterNode.StateMachineNode.SwitchState<PatrolState>();
            return;
        }

        Move();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);
        CharacterNode.NavigationAgent.TargetPosition = destination;
        CharacterNode.ChaseAreaNode.BodyShapeEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState() 
    {
        base.ExitState();
        CharacterNode.ChaseAreaNode.BodyShapeEntered -= HandleChaseAreaBodyEntered;
    }
}
