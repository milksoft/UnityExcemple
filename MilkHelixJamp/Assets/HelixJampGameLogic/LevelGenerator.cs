using UnityEngine;

namespace HelixJampGameLogic
{
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

        public void CreateLevelInterier(Transform Levelroot, int levelIndex)
        {
            int CountPlatforms = 5 * levelIndex;
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

        public SectorType GetSectorType(GameObject sector)
        {
            //var prefab = PrefabUtility.GetPrefabParent(sector);
            var m = sector.GetComponent<Material>();
            var m1 = levelResorce.PrefabSectorGood.GetComponent<Material>();
            if (m.GetInstanceID() == m1.GetInstanceID())
                return SectorType.Good;
            var m2 = levelResorce.PrefabSectorBad.GetComponent<Material>();
            if (m.GetInstanceID() == m2.GetInstanceID())
                return SectorType.Bad;

            return SectorType.None;
        }

        internal void ClearLevel(Transform levelroot)
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
            
           
            var sects = RandomisePlatform(r);

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
            //sector.GetPrefabDefinition();
            //if (sector.TryGetComponent(out Sector data))
            //    data.IsKiller = stype == SectorType.Bad;

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

        //private enum HardCore
        //{
        //    Easy,
        //    Midlle,
        //    Hard
        //}

        private SectorType[] RandomisePlatform(System.Random r/*, HardCore HardCore*/)
        {
            SectorType[] sectorTypes = new SectorType[sectorcount];
            bool iswindow = false;
            for (int i = 0; i < sectorcount; i++)
            {
                sectorTypes[i] = (SectorType)r.Next(3);
                if (sectorTypes[i] == SectorType.None)
                    iswindow = true;
            }

            if (!iswindow)
            {
                sectorTypes[0] = SectorType.None;
            }
            return sectorTypes;
        }
    }
}