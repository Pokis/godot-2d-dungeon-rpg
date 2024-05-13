using DungeonRPG.Scripts.General;
using Godot;
using System;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        RecalculateEnemies(0);
        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        RecalculateEnemies(1);
    }

    private void RecalculateEnemies(int delta)
    {
        var childCount = GetChildCount() - delta;
        GameEvents.RaiseNewEnemyCount(childCount);

        if(childCount == 0) 
        {
            GameEvents.RaiseVictory();
        }
    }
}
