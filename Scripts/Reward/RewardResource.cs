using DungeonRPG.Scripts.Resources;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Reward
{
    [GlobalClass]
    internal partial class RewardResource : Resource
    {
        [Export]
        public Texture2D SpriteTexture { get; private set; }

        [Export]
        public string Description { get; private set; }

        [Export]
        public StatType TargetStats { get; private set; }

        [Export(PropertyHint.Range,"1,100,1")]
        public float Amount { get; private set; }
    }
}
