using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Resources
{
    [GlobalClass]
    internal partial class StatResource : Resource
    {
        public event Action OnZero;
        public event Action OnUpdate;

        [Export]
        public StatType Type { get; private set; }

        private float _value;

        [Export]
        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(value, 0, Mathf.Inf);
                OnUpdate?.Invoke();

                if(_value == 0)
                {
                    //GD.Print($"{Type.ToString()} Resource reached 0.");
                    OnZero?.Invoke();
                }
                else
                {
                   // GD.Print($"{Type.ToString()} currently at {_value} Resource not 0.");
                }
            }
        }
    }
}
