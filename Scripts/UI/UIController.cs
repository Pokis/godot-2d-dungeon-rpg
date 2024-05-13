using DungeonRPG.Scripts.General;
using DungeonRPG.Scripts.UI;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

internal partial class UIController : Control
{
    internal Dictionary<ContainerType, UIContainer> Containers;

    private bool canPause = false;

    public override void _Ready()
    {
        Containers = GetChildren()
            .Where(e=> e is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary((element) => element.ContainerType);
        
        Containers[ContainerType.Start].Visible = true;
        Containers[ContainerType.Stats].Visible = false;

        Containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        GameEvents.OnGameEnd += GameEvents_OnGameEnd;
        GameEvents.OnVictory += GameEvents_OnVictory;
        GameEvents.OnReward += GameEvents_OnReward;
       
        Containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        Containers[ContainerType.Reward].ButtonNode.Pressed += RewardButtonPressed;
    }



    public override void _Input(InputEvent @event)
    {
        if(!canPause)
        {
            return;
        }

        if(!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }

        Containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        Containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void GameEvents_OnVictory()
    {
        canPause = false;
        Containers[ContainerType.Stats].Visible = false;
        Containers[ContainerType.Victory].Visible = true;

        GetTree().Paused = true;
    }

    private void GameEvents_OnGameEnd()
    {
        canPause = false;
        Containers[ContainerType.Stats].Visible = false;
        Containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        Containers[ContainerType.Start].Visible = false;
        Containers[ContainerType.Stats].Visible = true;
        

        GameEvents.RaiseStartGame();
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = false;
        Containers[ContainerType.Stats].Visible = true;
        Containers[ContainerType.Pause].Visible = false;
    }

    private void GameEvents_OnReward(DungeonRPG.Scripts.Reward.RewardResource reward)
    {
        canPause = false;
        GetTree().Paused = true;
        Containers[ContainerType.Stats].Visible = false;
        Containers[ContainerType.Reward].Visible = true;

        Containers[ContainerType.Reward].TextureNode.Texture = reward.SpriteTexture;
        Containers[ContainerType.Reward].LabelNode.Text = reward.Description;
    }

    private void RewardButtonPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        Containers[ContainerType.Stats].Visible = true;
        Containers[ContainerType.Reward].Visible = false;
    }
}
