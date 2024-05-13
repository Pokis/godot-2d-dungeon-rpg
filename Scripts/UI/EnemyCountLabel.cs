using DungeonRPG.Scripts.General;
using Godot;
using System;

public partial class EnemyCountLabel : Label
{
    public override void _Ready()
    {
        GD.Print("subscribing to new enemy count.");
        GameEvents.OnNewEnemyCount += GameEvents_OnNewEnemyCount;
    }

    private void GameEvents_OnNewEnemyCount(int newCount)
    {
        GD.Print("new enemy count retrieved");
        Text = newCount.ToString();
    }
}
