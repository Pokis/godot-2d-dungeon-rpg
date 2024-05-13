using DungeonRPG.Scripts.General;
using Godot;
using System;

internal partial class PlayerMoveState : PlayerState
{

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float speed = 5;

    public override void _PhysicsProcess(double delta)
	{
        if (CharacterNode.Direction == Vector2.Zero)
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        CharacterNode.Velocity = new(CharacterNode.Direction.X, 0, CharacterNode.Direction.Y);
        CharacterNode.Velocity *= speed;

        CharacterNode.MoveAndSlide();

        CharacterNode.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);
    }
}
