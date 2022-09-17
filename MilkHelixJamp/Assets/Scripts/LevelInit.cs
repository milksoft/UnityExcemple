using HelixJampGameLogic;

using UnityEngine;

public class LevelInit : MonoBehaviour, HelixJampGameLogic.ILevelResource, HelixJampGameLogic.ILevelBaseObjects//, IGameState
{
    public GameObject levelRoot;
    public GameObject player;
    public GameObject camera;

    public GameObject prefabPlateCancel;

    public GameObject prefabPlatform;

    public GameObject prefabSectorBad;

    //public int currentLevel;
    public GameObject prefabSectorGood;

    public GameObject prefabSterm;
    //public int CurrentLevel => currentLevel;

    #region ILevelResource

    GameObject ILevelBaseObjects.LevelRoot => levelRoot;
    GameObject ILevelBaseObjects.Player => player;
    GameObject ILevelBaseObjects.Camera => camera;

    GameObject HelixJampGameLogic.ILevelResource.PrefabPlateCancel => prefabPlateCancel;
    GameObject HelixJampGameLogic.ILevelResource.PrefabPlatform => prefabPlatform;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSectorBad => prefabSectorBad;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSectorGood => prefabSectorGood;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSterm => prefabSterm;

    #endregion ILevelResource

    private void Awake()
    {
        HelixJampGameLogic.GameState.InitGame(this, this);
        //LevelGenerator levelGenerator = new LevelGenerator(this, this);
        //levelGenerator.CreateLevelInterier(this.gameObject.transform);
    }

    //internal Platform CurrentPlatform;
    private void Start()
    {
        HelixJampGameLogic.GameState.Game.StartNewGame();
    }
}