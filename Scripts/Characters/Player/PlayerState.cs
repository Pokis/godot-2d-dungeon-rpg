using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.General;
using DungeonRPG.Scripts.Resources;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


internal abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        CharacterNode.GetStatResource(StatType.Health).OnZero += HandleZeroHealth;
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_Attack))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }
}

