using DungeonRPG.Scripts.Reward;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.General
{
    internal class GameEvents
    {
        public static event Action OnStartGame;
        public static event Action OnGameEnd;
        public static event Action OnVictory;
        public static event Action<int> OnNewEnemyCount;
        public static event Action<RewardResource> OnReward;

        public static void RaiseStartGame() => OnStartGame?.Invoke();
        public static void RaiseGameEnd() => OnGameEnd?.Invoke();
        public static void RaiseVictory() => OnVictory?.Invoke();
        public static void RaiseNewEnemyCount(int count)
        {
            OnNewEnemyCount?.Invoke(count);
        }

        public static void RaiseReward(RewardResource resource)
        {
            OnReward?.Invoke(resource);
        }
    }
}
