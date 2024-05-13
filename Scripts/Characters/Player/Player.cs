using DungeonRPG.Scripts.Characters;
using DungeonRPG.Scripts.General;
using Godot;
using System;
using static Godot.TextServer;

internal partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += GameEvents_OnReward;
    }

    private void GameEvents_OnReward(DungeonRPG.Scripts.Reward.RewardResource reward)
    {
        var playerStats = GetStatResource(reward.TargetStats);

        if(playerStats != null)
        {
            playerStats.Value += reward.Amount;
        }
    }
}
