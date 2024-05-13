using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Characters.EnemyKnight
{
    internal abstract partial class EnemyState : CharacterState
    {
        protected Vector3 destination { get; set; }

        public override void _Ready()
        {
            base._Ready();
            CharacterNode.GetStatResource(Resources.StatType.Health).OnZero += HandleZeroHealth;
        }

        protected Vector3 GetPointGlobalPosition(int index)
        {
            var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(index);
            var globalPosition = CharacterNode.PathNode.GlobalPosition;

            return globalPosition + localPosition;
        }

        protected void Move()
        {
            CharacterNode.NavigationAgent.GetNextPathPosition();
            CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(destination);

            CharacterNode.MoveAndSlide();
            CharacterNode.Flip();
        }

        protected void HandleChaseAreaBodyEntered(Rid bodyRid, Node3D body, long bodyShapeIndex, long localShapeIndex)
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }

        private void HandleZeroHealth()
        {
            GD.Print("Enemy zero health reached.");
            CharacterNode.StateMachineNode.SwitchState<EnemyDeathState>();
        }
    }
}
