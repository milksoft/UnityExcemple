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
        private StateUi stateUi = StateUi.StartScreen;

        public event Action<GameState> OnGameStateChanged;

        public event Action<StateUi> OnGameStateUIChanged;

        public GameInstanceState CurrentLevel { get; private set; }

        public HardCore HardCore { get; set; }

        public int MaximumScore { get; private set; }

        public int RecordScore { get; private set; }

        public StateUi StateUi
        {
            get => stateUi;
            set
            {
                stateUi = value;
                OnGameStateUIChanged?.Invoke(stateUi);
            }
        }

        public void LevelPlay(bool isReplay)
        {
            if (MaximumScore > RecordScore)
                RecordScore = MaximumScore;

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