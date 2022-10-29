using System;

using UnityEngine;

using static UiView;

public class GameStateObject : MonoBehaviour
{
    public GameObject[] LevelsPrefabs;
    public int StartGameHealth = 3;
    private int _score;
    private StateUi stateUi;

    public event Action<GameStateObject> OnGameStateChanged;

    public event Action<StateUi> OnGameStateUIChanged;

    public int CurrentLevelIndex { get; private set; }
    public LevelDataBase Level { get; private set; }
    public int MaxLevelIndex => LevelsPrefabs.Length;
    public int RecordScore { get; internal set; }

    public int Score
    {
        get => _score;
        internal set
        {
            _score = value;
            OnGameStateChanged(this);
        }
    }

    public Player Snake { get; private set; }
    public float Speed { get; internal set; } = 30;

    public StateUi StateUi
    {
        get => stateUi;
        private set
        {
            stateUi = value;
            OnGameStateUIChanged?.Invoke(stateUi);
        }
    }

    public static GameStateObject GetInstance(GameObject go) => go.transform.GetComponentInParent<GameStateObject>();

    internal void PlayerDied()
    {
        if (Score > RecordScore)
            RecordScore = Score;

        StateUi = StateUi.GameOver;
        Score = 0;
    }

    internal void PlayerWin()
    {
        if (Score > RecordScore)
            RecordScore = Score;
        OnGameStateChanged(this);
        StateUi = StateUi.GamePaused;
    }

    internal void StartGame(bool isNewgame)
    {
        if (isNewgame)
            CurrentLevelIndex = 0;
        CurrentLevelIndex++;
        OnGameStateChanged(this);
        CreateLevel();
        Snake.PlayerInit(isNewgame);
        StateUi = StateUi.GamePlaying;
    }

    private void Awake()
    {
        StateUi = StateUi.StartScreen;
    }

    private void CreateLevel()
    {
        if (Level != null)
        {
            Destroy(Level.gameObject);
            Level = null;
        }
        var ind = (CurrentLevelIndex - 1) % MaxLevelIndex;

        GameObject platform = Instantiate(LevelsPrefabs[ind], this.transform);

        Level = platform.GetComponent<LevelDataBase>();
        Snake = GetComponentInChildren<Player>();
    }
}