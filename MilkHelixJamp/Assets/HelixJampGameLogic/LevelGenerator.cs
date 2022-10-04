using UnityEngine;

namespace HelixJampGameLogic
{
    public interface ILevelResource
    {
        GameObject PrefabPlateCancel { get; }
        GameObject PrefabPlatform { get; }
        GameObject PrefabSectorBad { get; }
        GameObject PrefabSectorGood { get; }
        GameObject PrefabSterm { get; }
    }

    public class LevelGenerator
    {
        private const float platformBetweenDistance = 2;
        private const float sectorangle = 360 / sectorcount;
        private const int sectorcount = 12;
        private readonly ILevelResource levelResorce;

        public LevelGenerator(ILevelResource levelResorce)
        {
            this.levelResorce = levelResorce;
        }

        public enum SectorType
        {
            None,
            Good,
            Bad
        }

        private enum HardCore
        {
            Easy,
            Midlle,
            Hard
        }

        public void CreateLevelInterier(Transform Levelroot, int levelIndex)
        {
            int CountPlatforms = GetAllCountPlatforms(levelIndex);
            System.Random rand = new System.Random(levelIndex);
            int levellength = CountPlatforms * 2;

            GameObject sterm = GameObject.Instantiate(levelResorce.PrefabSterm);
            sterm.transform.localScale = new Vector3(1, CountPlatforms, 1);
            sterm.transform.localPosition = new Vector3(0, 0.1f, 0);
            sterm.transform.SetParent(Levelroot, false);

            for (int i = 0; i < CountPlatforms; i++)
            {
                var platform = CreatePlatform(i, rand, Levelroot);
            }

            GameObject plate = GameObject.Instantiate(levelResorce.PrefabPlateCancel);
            plate.transform.localPosition = new Vector3(0, -levellength, 0);
            plate.transform.SetParent(Levelroot, false);
        }

        public static int GetAllCountPlatforms(int levelIndex) => 5 * levelIndex;

        public void ClearLevel(Transform levelroot)
        {
            for (int i = 0; i < levelroot.childCount; i++)
            {
                GameObject.Destroy(levelroot.GetChild(i).gameObject);
            }
        }

        private GameObject CreatePlatform(int platformindex, System.Random r, Transform Levelroot)
        {
            var pos = new Vector3(0, -platformBetweenDistance * platformindex, 0);

            GameObject platform = GameObject.Instantiate(levelResorce.PrefabPlatform, Levelroot);

            var sects = RandomisePlatform(r, HardCore.Easy);

            if (platformindex == 0)
                sects[0] = SectorType.Good;// spawn point

            for (int i = 0; i < sectorcount; i++)
            {
                var newsector = CreateSector(sects[i], i, platform.transform);
            }

            platform.transform.localPosition = pos;

            return platform;
        }

        private GameObject CreateSector(SectorType stype, int index, Transform parent)
        {
            GameObject prefab = FactoryPrefabModel(stype);
            if (prefab == null)
                return null;

            GameObject sector = GameObject.Instantiate(prefab, parent);
            var rotate = Quaternion.Euler(0, index * sectorangle, 0);
            sector.transform.localRotation = rotate;
            var SectorInfo = sector.GetComponent<Sector>();
            SectorInfo.SectorType = stype;

            return sector;
        }

        private GameObject FactoryPrefabModel(SectorType i)
        {
            switch (i)
            {
                case SectorType.Good:
                    return levelResorce.PrefabSectorGood;

                case SectorType.Bad:
                    return levelResorce.PrefabSectorBad;

                default:
                    return null;
            }
        }

        private SectorType[] RandomisePlatform(System.Random r, HardCore hardCore)
        {
            SectorType[] sectorTypes = new SectorType[sectorcount];
            bool iswindow = false;

            switch (hardCore)
            {
                case HardCore.Easy:
                    {
                        bool isbad = false;
                        for (int i = 0; i < sectorcount; i++)
                        {
                            sectorTypes[i] = (SectorType)r.Next(isbad ? 2 : 3);
                            if (sectorTypes[i] == SectorType.None)
                                iswindow = true;
                            if (sectorTypes[i] == SectorType.Bad)
                                isbad = true;
                        }
                    }
                    break;

                case HardCore.Midlle:
                    for (int i = 0; i < sectorcount; i++)
                    {
                        sectorTypes[i] = (SectorType)r.Next(3);
                        if (sectorTypes[i] == SectorType.None)
                            iswindow = true;
                    }
                    break;

                case HardCore.Hard:
                    for (int i = 0; i < sectorcount; i++)
                    {
                        sectorTypes[i] = SectorType.Bad;
                        if (sectorTypes[i] == SectorType.None)
                            iswindow = true;
                    }
                    break;
            }

            if (!iswindow)
            {
                sectorTypes[0] = SectorType.None;
            }

            return sectorTypes;
        }
    }
}