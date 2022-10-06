using HelixJampGameLogic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    public GameObject GameOverView;
    public TMP_Text levelcurrent;
    public TMP_Text levelnext;
    public GameObject NextView;
    public TMP_Text Score;
    public TMP_Text ScoreAll;
    public GameObject ScoreAllView;
    public Slider Scorenext;
    public TMP_Text ScoreRecord;
    public GameObject ScoreView;
    public GameObject StartView;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameEasy()
    {
        GameInputControler.Instance.GameState.HardCore = HardCore.Easy;
        GameInputControler.Instance.StartNextLevel();
    }

    public void StartGameHard()
    {
        GameInputControler.Instance.GameState.HardCore = HardCore.Hard;
        GameInputControler.Instance.StartNextLevel();
    }

    public void StartGameMidle()
    {
        GameInputControler.Instance.GameState.HardCore = HardCore.Midlle;
        GameInputControler.Instance.StartNextLevel();
    }

    public void StartNext()
    {
        GameInputControler.Instance.StartNextLevel();
    }

    public void StartReplay()
    {
        GameInputControler.Instance.StartReplayGame();
    }

    private void CurrentLevel_OnGameStateChanged(GameInstanceState obj)
    {
        Scorenext.value = GameInputControler.Instance.GameState.CurrentLevel.Score;
        Score.text = $"����: {GameInputControler.Instance.GameState.CurrentLevel.Score}";
    }

    private void OnGameStateChanged(GameState obj)
    {
        GameInputControler.Instance.GameState.CurrentLevel.OnGameStateChanged += CurrentLevel_OnGameStateChanged;
        Scorenext.maxValue = GameInputControler.Instance.GameState.CurrentLevel.AllPlatformCount;
        levelcurrent.text = GameInputControler.Instance.GameState.CurrentLevel.CurrentLevelIndex.ToString();
        levelnext.text = (GameInputControler.Instance.GameState.CurrentLevel.CurrentLevelIndex + 1).ToString();
        ScoreAll.text = $"����� �������:\r\n {GameInputControler.Instance.GameState.MaximumScore}";
        ScoreRecord.text = $"������:\r\n {GameInputControler.Instance.GameState.RecordScore}";
    }

    private void OnGameStateUIChanged(StateUi state)
    {
        switch (state)
        {
            case StateUi.StartScreen:
                StartView.SetActive(value: true);
                ScoreAllView.SetActive(value: false);
                ScoreView.SetActive(value: false);
                NextView.SetActive(value: false);
                GameOverView.SetActive(value: false);
                break;

            case StateUi.GamePlaying:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: false);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: false);
                GameOverView.SetActive(value: false);
                break;

            case StateUi.GamePaused:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: true);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: true);
                GameOverView.SetActive(value: false);
                break;

            case StateUi.GameOver:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: true);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: false);
                GameOverView.SetActive(value: true);
                break;
        }
    }

    private void Start()
    {
        GameInputControler.Instance.GameState.OnGameStateChanged += OnGameStateChanged;
        GameInputControler.Instance.GameState.OnGameStateUIChanged += OnGameStateUIChanged;
    }
}