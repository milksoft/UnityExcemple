using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    public AudioSource buttonclickSound;

    public GameStateObject game;

    public GameObject GameOverView;

    public AudioSource MainSongSound;

    public GameObject NextView;

    public TMP_Text Score;

    public GameObject ScoreAllView;

    public TMP_Text ScoreRecord;

    public GameObject ScoreView;

    public GameObject StartView;

    public enum StateUi
    {
        StartScreen,
        GamePlaying,
        GamePaused,
        GameOver,
    }

    private void bindButtonClick()
    {
        void ExitGame()
        {
            buttonclickSound.Play();
            Application.Quit();
        }

        void StartGame()
        {
            buttonclickSound.Play();
            game.StartGame(true);
        }

        void StartNext()
        {
            buttonclickSound.Play();
            game.StartGame(false);
        }

        GameOverView.transform.Find("Canvas/ButtonExit").GetComponent<Button>().onClick.AddListener(ExitGame);
        StartView.transform.Find("Canvas/ButtonExit").GetComponent<Button>().onClick.AddListener(ExitGame);
        NextView.transform.Find("Canvas/ButtonExit").GetComponent<Button>().onClick.AddListener(ExitGame);

        GameOverView.transform.Find("Canvas/ButtonReplay")?.GetComponent<Button>().onClick.AddListener(StartGame);
        StartView.transform.Find("Canvas/ButtonMidle").GetComponent<Button>().onClick.AddListener(StartGame);
        NextView.transform.Find("Canvas/ButtonReplay").GetComponent<Button>().onClick.AddListener(StartNext);
    }

    private void OnGameStateChanged(GameStateObject obj)
    {
        ScoreRecord.text = $"–екорд:\r\n {game.RecordScore}";
        Score.text = $"—чет: {game.Score}";
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
                MainSongSound.Play();
                break;

            case StateUi.GamePlaying:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: false);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: false);
                GameOverView.SetActive(value: false);
                MainSongSound.Stop();
                break;

            case StateUi.GamePaused:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: true);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: true);
                GameOverView.SetActive(value: false);
                MainSongSound.Play();
                break;

            case StateUi.GameOver:
                StartView.SetActive(value: false);
                ScoreAllView.SetActive(value: true);
                ScoreView.SetActive(value: true);
                NextView.SetActive(value: false);
                GameOverView.SetActive(value: true);
                MainSongSound.Play();
                break;
        }
    }

    private void Start()
    {
        game.OnGameStateChanged += OnGameStateChanged;
        game.OnGameStateUIChanged += OnGameStateUIChanged;
        bindButtonClick();
        OnGameStateUIChanged(game.StateUi);
    }
}