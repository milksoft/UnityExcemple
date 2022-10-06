using HelixJampGameLogic;

using UnityEngine;

public class Sector : MonoBehaviour
{
    public HelixJampGameLogic.SectorType SectorType { get; set; }

    public void PlayerBounce(Rigidbody player)
    {
        const float Bouncespeed = 12;
        player.velocity = new Vector3(0, Bouncespeed, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled && collision.collider.TryGetComponent(out Rigidbody player))
        {
            if (SectorType == SectorType.Bad)
            {
                GameInputControler.Instance.StopGame(false);
            }
            else
            {
                PlayerBounce(player);
            }
        }
    }
}