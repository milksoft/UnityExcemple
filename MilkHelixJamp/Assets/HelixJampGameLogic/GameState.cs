using System;

using UnityEngine;

namespace HelixJampGameLogic
{
    public class GameInstanceState
    {
        public GameInstanceState(int currentLevelIndex)
        {
            CurrentLevelIndex = currentLevelIndex;
            AllPlatformCount = LevelGenerator.GetAllCountPlatforms(currentLevelIndex);
            OnGameStateChanged?.Invoke(this);
        }

        public event Action<GameInstanceState> OnGameStateChanged;

        public int AllPlatformCount { get; }

        public int CurrentLevelIndex { get; }
        public Transform CurrentPlatform { get; set; }
        public int Score { get; private set; } = 0;

        internal void AddScore()
        {
            Score++;
            OnGameStateChanged?.Invoke(this);
        }
    }

    public class GameState
    {
        private int _CurrentLevelIndex = 0;

        public event Action<GameState> OnGameStateChanged;

        public GameInstanceState CurrentLevel { get; private set; }
        public int MaximumScore { get; private set; }

        public void LevelPlay(bool isReplay)
        {
            if (!isReplay)
            {
                _CurrentLevelIndex++;
                MaximumScore += CurrentLevel?.Score ?? 0;
            }

            CurrentLevel = new GameInstanceState(_CurrentLevelIndex);
            OnGameStateChanged?.Invoke(this);
        }
    }
}