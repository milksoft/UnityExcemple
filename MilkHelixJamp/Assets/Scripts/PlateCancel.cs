using UnityEngine;

public class PlateCancel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameInputControler.Instance.IsPlayer(collision.gameObject))
        {
            GameInputControler.Instance.StartNewGame();
        }
    }
}