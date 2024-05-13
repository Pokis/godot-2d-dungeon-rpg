using DungeonRPG.Scripts.General;
using DungeonRPG.Scripts.Interfaces;
using DungeonRPG.Scripts.Resources;
using Godot;
using System;
using System.Linq;

namespace DungeonRPG.Scripts.Characters
{
    internal abstract partial class Character : CharacterBody3D
    {

        [Export]
        private StatResource[] Stats;

        [ExportGroup("Required nodes")]

        [Export]
        public AnimationPlayer AnimationPlayerNode { get; private set; }

        [Export]
        public Sprite3D Sprite3DNode { get; private set; }

        [Export]
        public StateMachine StateMachineNode { get; private set; }

        [Export]
        public Area3D HitBox { get; private set; }
        [Export]
        public CollisionShape3D HitBoxShapeNode { get; private set; }

        [Export]
        public Timer ShaderTimerNode { get; private set; }


        [ExportGroup("AI Nodes")]
       
        [Export]
        public Path3D PathNode { get; private set; }
        [Export]
        public Area3D ChaseAreaNode { get; private set; }
        [Export]
        public Area3D AttackArea { get; private set; }
        [Export]
        public NavigationAgent3D NavigationAgent { get; private set; }
        [Export]
        public Area3D HurtBox { get; private set; }

        public Vector2 Direction { get; private set; } = new Vector2();


        private ShaderMaterial shader;

        public override void _Ready()
        {
            shader = (ShaderMaterial) Sprite3DNode.MaterialOverlay;

            AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
            
            if(HitBox != null)
            {
                HitBox.AreaEntered += HitBox_AreaEntered;
            }

            if(HurtBox != null)
            {
                HurtBox.AreaEntered += HurtBoxAreaEntered;
            }

            Sprite3DNode.TextureChanged += Sprite3DNode_TextureChanged;
            ShaderTimerNode.Timeout += HandleShaderTimeout;
        }



        public override void _Input(InputEvent @event)
        {
            Direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
        }

        public void Flip()
        {
            bool isNotMovingHorizontally = Velocity.X == 0;

            if (isNotMovingHorizontally) { return; }

            bool isMovingLeft = Velocity.X < 0;
            Sprite3DNode.FlipH = isMovingLeft;
        }

        private void HitBox_AreaEntered(Area3D body)
        {
            GD.Print("Area entered hitbox!");
        }

        public void SetHitBoxPosition(Vector3 position)
        {
            HitBoxShapeNode.GlobalPosition = position;
        }

        public void ToggleHitBox (bool value)
        {
            HitBoxShapeNode.Disabled = value;
        }

        private void HurtBoxAreaEntered(Area3D area)
        {
            if(area is not IHitbox attackHitbox)
            {
                return;
            }

            var health = GetStatResource(StatType.Health);

            var damage = attackHitbox.GetDamage();

            health.Value -= damage;

            GD.Print($"Health of {Name}: {health.Value}");

            shader.SetShaderParameter("active", true);
            ShaderTimerNode.Start();
        }

        public StatResource? GetStatResource(StatType type)
        {
            return Stats.FirstOrDefault(r => r.Type == type);
        }

        private void Sprite3DNode_TextureChanged()
        {
            shader.SetShaderParameter("tex", Sprite3DNode.Texture);
        }

        private void HandleShaderTimeout()
        {
            shader.SetShaderParameter("active", false);
        }
    }
}
