using HelixJampGameLogic;

using UnityEngine;

public class GameInputControler : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject player;
    public GameObject RotationRoot;
   
    private readonly Vector3 _CameraViewOffset = new Vector3(0, 3, 18);
    private LevelGenerator _Generator;
    private LevelInit _LevelViewData;
    private Vector3 _previousPosition = default;
   
    public static GameInputControler Instance { get; private set; }
   
    public GameState GameState { get; private set; }


    public void CameraControlUpdateLoop()
    {
        const float SpeedViewScroll = 30f;

        if (GameState.CurrentLevel.CurrentPlatform == null)
            return;
        var targetpos = GameState.CurrentLevel.CurrentPlatform.position + _CameraViewOffset;
        MainCamera.transform.position =
            Vector3.MoveTowards(MainCamera.transform.position, targetpos, SpeedViewScroll * Time.deltaTime);
    }

    public bool IsPlayer(GameObject other)
    {
        return other.GetInstanceID() == player.GetInstanceID();
    }

    public void MouseInputControlUpdateLoop()
    {
        const float sensetive = 0.3f;

        if (Input.GetMouseButton(0))
        {
            var d = Input.mousePosition - _previousPosition;
            var angleret = -d.x * sensetive;
            RotationRoot.transform.Rotate(0, angleret, 0);
        }
        _previousPosition = Input.mousePosition;
    }

    public void StartNewGame()
    {
        GameState.LevelPlay(false);
        _Generator.ClearLevel(RotationRoot.transform);
        _Generator.CreateLevelInterier(RotationRoot.transform, GameState.CurrentLevel.CurrentLevelIndex);
        ClearPlayer();
    }

    public void StopGame()
    {
        GameState.LevelPlay(true);
        _Generator.ClearLevel(RotationRoot.transform);
        _Generator.CreateLevelInterier(RotationRoot.transform, GameState.CurrentLevel.CurrentLevelIndex);
        ClearPlayer();
    }

    private void Awake()
    {
        Initialise();
        Instance = this;
    }

    private void ClearPlayer()
    {
        player.transform.localPosition = new Vector3(0, 1, 5);
        MainCamera.transform.position = RotationRoot.transform.position;
        RotationRoot.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Initialise()
    {
        _LevelViewData = RotationRoot?.GetComponent<LevelInit>();
        if (_LevelViewData == null)
            throw new System.ApplicationException("Not LevelInit");

        _Generator = new LevelGenerator(_LevelViewData);
        GameState = new GameState();
        //HelixJampGameLogic.GameState.InitGame(this, LevelViewData);
    }

    private void Start()
    {
        StartNewGame();
    }

    private void Update()
    {
        CameraControlUpdateLoop();
        MouseInputControlUpdateLoop();
    }
}