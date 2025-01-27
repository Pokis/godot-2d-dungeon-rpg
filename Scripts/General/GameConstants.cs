﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.General
{
    internal class GameConstants
    {
        public const string ANIMATION_IDLE = "Idle";
        public const string ANIMATION_MOVE = "Move";
        public const string ANIMATION_DASH = "Dash";
        public const string ANIMATION_ATTACK = "Attack";
        public const string ANIMATION_DEATH = "Death";
        public const string ANIMATION_EXPAND = "Expand";
        public const string ANIMATION_EXPLOSION = "Explosion";
        public const string ANIMATION_STUN = "Stun";

        public const string INPUT_MOVE_LEFT = "MoveLeft";
        public const string INPUT_MOVE_RIGHT = "MoveRight";
        public const string INPUT_MOVE_FORWARD = "MoveForward";
        public const string INPUT_MOVE_BACKWARD = "MoveBackward";
        public const string INPUT_DASH = "Dash";
        public const string INPUT_Attack = "Attack";
        public const string INPUT_PAUSE = "Pause";
        public const string INPUT_INTERACT = "Interact";

        //Notifications
        public const int NOTIFICATION_ENTER_STATE = 5001;
        public const int NOTIFICATION_EXIT_STATE = 5002;
    }
}
