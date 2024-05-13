using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.General;
using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	[Export]
	private Node CurrentState; 
	
	[Export]
	private CharacterState[] States;

	public override void _Ready()
	{
		CurrentState.Notification(5001);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SwitchState<T>() where T : Node
	{
		var newState = States.FirstOrDefault(s=> s is T);

		if(newState is null)
		{
			return;
		}

		if(CurrentState is T)
		{
			return;
		}

		if (!newState.CanTransition()) { return; }

		CurrentState?.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
		CurrentState = newState;
        CurrentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
