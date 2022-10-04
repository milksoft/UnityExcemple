using HelixJampGameLogic;

using UnityEngine;

public class Sector : MonoBehaviour
{
    public HelixJampGameLogic.LevelGenerator.SectorType SectorType { get; set; }

    public void OnGoodSectorToPlayerCollision(GameObject sector, Collision collision)
    {
        const float unlockdistance = 0.5f;

        if (collision.collider.TryGetComponent(out Rigidbody player))
        {
            var normal = -collision.contacts[0].normal.normalized;
            var dot = Vector3.Dot(normal, Vector3.up);
            if (dot >= unlockdistance)
            {
                if (SectorType == LevelGenerator.SectorType.Bad)
                    GameInputControler.Instance.StopGame();
                else
                    PlayerBounce(player);
            }
        }
    }

    public void PlayerBounce(Rigidbody player)
    {
        const float Bouncespeed = 12;
        player.velocity = new Vector3(0, Bouncespeed, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (SectorType == LevelGenerator.SectorType.Bad)
            GameInputControler.Instance.StopGame();
        else
            if (collision.collider.TryGetComponent(out Rigidbody player))
            PlayerBounce(player);
        //OnGoodSectorToPlayerCollision(this.gameObject, collision);
    }
}