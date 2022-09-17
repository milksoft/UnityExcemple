using UnityEngine;

namespace HelixJampGameLogic
{
    public interface ILevelBaseObjects
    {
        GameObject LevelRoot { get; }
        GameObject Player { get; }
        GameObject Camera { get; }
    }

    public interface ILevelResource
    {
        GameObject PrefabPlateCancel { get; }
        GameObject PrefabPlatform { get; }
        GameObject PrefabSectorBad { get; }
        GameObject PrefabSectorGood { get; }
        GameObject PrefabSterm { get; }
    }

    //internal class GameInputControler
    //{
       
    //}

    internal class GameInstanceState
    {
        private readonly LevelInit InstanceLevel;
        private int Score;

        public GameInstanceState(GameState mainGameData)
        {
            //Input = new GameInputControler(this);
            Interaction = new InteractivatingObjects(this);
            MainGameData = mainGameData;
        }

        public Transform CurrentPlatform { get; internal set; }
        //public GameInputControler Input { get; }
        public InteractivatingObjects Interaction { get; }
        public GameState MainGameData { get; }
    }

    internal class GameState
    {
        private int currentLevelIndex = 0;

        public static GameState Game { get; private set; }

        public GameInstanceState CurrentLevel { get; private set; }

        public LevelGenerator Generator { get; private set; }

        public Transform Levelroot { get; private set; }

        public int MaximumScore { get; private set; }

        public Rigidbody PlayerRigidbody { get; private set; }

        public static void InitGame(ILevelBaseObjects levelbase,ILevelResource levelResource )
        {
            Game = new GameState();
            Game.Levelroot = levelbase.LevelRoot.transform;
            Game.PlayerRigidbody = levelbase.Player.GetComponent<Rigidbody>();

            Game.Generator = new LevelGenerator(levelResource);
        }

        public void StartNewGame()
        {
            currentLevelIndex++;
            CurrentLevel = new GameInstanceState(this);
            Generator.ClearLevel(Levelroot.transform);
            Generator.CreateLevelInterier(Levelroot.transform, currentLevelIndex);
        }
    }

    internal class InteractivatingObjects
    {
        //private readonly Rigidbody phisPlayer;
        private readonly GameInstanceState gameInstance;

        public InteractivatingObjects(GameInstanceState gameInstance)
        {
            this.gameInstance = gameInstance;
        }

        public void OnNewStep(GameObject other,Collider bullet)
        {
            //if (other.TryGetComponent(out Rigidbody player))
            //{
                //this.gameObject.GetComponentInParent<LevelInit>().
                gameInstance.CurrentPlatform = other.gameObject.transform;
                //player.CurrentPlatform = this;
            //}
        }

        public void OnSectorToPlayerCollision(Collision collision)
        {
            const float unlockdistance = 0.5f;

            if (collision.collider.TryGetComponent(out Rigidbody player))
            {
                var normal = -collision.contacts[0].normal.normalized;
                var dot = Vector3.Dot(normal, Vector3.up);
                if (dot >= unlockdistance)
                {
                    var stype = gameInstance.MainGameData.Generator.GetSectorType(collision.gameObject);
                    if (stype == LevelGenerator.SectorType.Bad)
                        Debug.Log("gameover");
                    else
                    {
                        PlayerBounce(player);
                    }
                }
            }
        }

        public void PlayerBounce(Rigidbody player)
        {
            const float Bouncespeed = 12;
            player.velocity = new Vector3(0, Bouncespeed, 0);
        }
    }
}