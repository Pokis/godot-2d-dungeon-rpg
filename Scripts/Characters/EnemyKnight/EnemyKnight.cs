using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.Interfaces;
using Godot;
using System;

internal partial class EnemyKnight : Character
{
    public override void _Ready()
    {
        base._Ready();

        HurtBox.AreaEntered += HurtBox_AreaEntered;
    }

    private void HurtBox_AreaEntered(Area3D area)
    {
        if(area is not IHitbox hitbox)
        {
            return;
        }

        if (hitbox.CanStun() && GetStatResource(DungeonRPG.Scripts.Resources.StatType.Health).Value != 0)
        {
            StateMachineNode.SwitchState<StunState>();
        }
    }
}
