using DungeonRPG.Scripts.Characters.EnemyKnight;
using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class PatrolState : EnemyState
{
    private int pointIndex = 0;
    [Export]
    private Timer IdleTimer { get; set; }

    private RandomNumberGenerator RandomNumberGenerator { get; set; }

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float MaxIdleTime = 4;

    public override void _Ready()
    {
        base._Ready();
        RandomNumberGenerator = new RandomNumberGenerator();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);

        pointIndex = 1;
        destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.NavigationAgent.TargetPosition = destination;
        CharacterNode.NavigationAgent.NavigationFinished += HandleNavigationFinished;
        IdleTimer.Timeout += HandleIdleTimeout;
        CharacterNode.ChaseAreaNode.BodyShapeEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();
        CharacterNode.NavigationAgent.NavigationFinished -= HandleNavigationFinished;
        CharacterNode.ChaseAreaNode.BodyShapeEntered -= HandleChaseAreaBodyEntered;
        IdleTimer.Timeout -= HandleIdleTimeout;
    }


    public override void _PhysicsProcess(double delta)
    {
        if (!IdleTimer.IsStopped())
        {
            return;
        }
        Move();
    }

    private void HandleNavigationFinished()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);

        IdleTimer.WaitTime = RandomNumberGenerator.RandfRange(0, MaxIdleTime);
        IdleTimer.Start();
    }

    private void HandleIdleTimeout()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, CharacterNode.PathNode.Curve.PointCount);
        destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.NavigationAgent.TargetPosition = destination;
    }

}
