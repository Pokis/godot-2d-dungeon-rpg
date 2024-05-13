using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.Interfaces;
using DungeonRPG.Scripts.Resources;
using Godot;
using System;

public partial class AttackHitBox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return false;
    }

    public float GetDamage() 
    {
        return GetOwner<Character>().GetStatResource(StatType.Strength).Value;
    }
}
