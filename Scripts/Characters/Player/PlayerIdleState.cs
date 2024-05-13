using DungeonRPG.Scripts.General;
using Godot;

internal partial class PlayerIdleState : PlayerState
{

    public override void _PhysicsProcess(double delta)
	{
        if (CharacterNode.Direction != Vector2.Zero)
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
        base._PhysicsProcess(delta);
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
