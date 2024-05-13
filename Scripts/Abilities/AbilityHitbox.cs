using DungeonRPG.Scripts.Abilities;
using DungeonRPG.Scripts.Interfaces;
using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class AbilityHitbox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return true;
    }

    public float GetDamage()
    {
        return GetOwner<AbilityBase>().Damage;
    }
}
