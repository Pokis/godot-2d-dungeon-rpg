using DungeonRPG.Scripts.Resources;
using Godot;
using System;

public partial class StatsHealthLabel : Label
{
    [Export]
    internal StatResource HealthStatResource { get; set; }

    public override void _Ready()
    {
        base._Ready();
        HealthStatResource.OnUpdate += HealthStatResource_OnUpdate;
        this.Text = HealthStatResource.Value.ToString();
    }

    private void HealthStatResource_OnUpdate()
    {
        this.Text = HealthStatResource.Value.ToString();
    }
}
