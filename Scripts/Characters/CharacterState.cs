using DungeonRPG.Scripts.General;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Characters
{
    internal abstract partial class CharacterState : Node
    {
        protected Character CharacterNode;
        public Func<bool> CanTransition = () => true;

        public override void _Ready()
        {
            CharacterNode = GetOwner<Character>();
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }

        public override void _Notification(int what)
        {
            if (what == GameConstants.NOTIFICATION_ENTER_STATE)
            {
                SetPhysicsProcess(true);
                SetProcessInput(true);
                EnterState();
            }
            else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
            {
                SetPhysicsProcess(false);
                SetProcessInput(false);
                ExitState();
            }

            base._Notification(what);
        }

        protected abstract void EnterState();
        protected virtual void ExitState() { }
    }
}
