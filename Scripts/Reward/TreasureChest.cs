using DungeonRPG.Scripts.General;
using DungeonRPG.Scripts.Reward;
using Godot;
using System;
using System.Linq;

public partial class TreasureChest : StaticBody3D
{
    [Export]
    private Area3D areaNode;

    [Export]
    private Sprite3D spriteNode;

    [Export]
    private RewardResource reward;

    public override void _Ready()
    {
        areaNode.BodyEntered += AreaNode_BodyEntered;
        areaNode.BodyExited += AreaNode_BodyExited;
    }

    public override void _Input(InputEvent @event)
    {
        if(!areaNode.Monitoring)
        {
            return;
        }

        if (!areaNode.GetOverlappingBodies().Any())
        {
            return;
        }

        if(Input.IsActionJustPressed(GameConstants.INPUT_INTERACT))
        {
            areaNode.Monitoring = false;
            GameEvents.RaiseReward(reward);  
        }
    }

    private void AreaNode_BodyExited(Node3D body)
    {
        spriteNode.Visible = false;
        GD.Print("Body to chest exited");
    }

    private void AreaNode_BodyEntered(Node3D body)
    {
        spriteNode.Visible = true;
        GD.Print("Body to chest entered");
    }
}
