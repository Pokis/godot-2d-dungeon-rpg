using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Abilities
{
    public partial class AbilityBase : Node3D
    {
        [Export(PropertyHint.Range, "0,100,1")]
        public float Damage { get; set; } = 10;

        [Export]
        protected AnimationPlayer playerNode;
    }
}
