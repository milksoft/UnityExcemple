using UnityEngine;

public class LevelInit : MonoBehaviour, HelixJampGameLogic.ILevelResource
{
    public GameObject prefabPlateCancel;

    public GameObject prefabPlatform;

    public GameObject prefabSectorBad;

    public GameObject prefabSectorGood;

    public GameObject prefabSterm;

    #region ILevelResource

    GameObject HelixJampGameLogic.ILevelResource.PrefabPlateCancel => prefabPlateCancel;
    GameObject HelixJampGameLogic.ILevelResource.PrefabPlatform => prefabPlatform;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSectorBad => prefabSectorBad;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSectorGood => prefabSectorGood;
    GameObject HelixJampGameLogic.ILevelResource.PrefabSterm => prefabSterm;

    #endregion ILevelResource
}