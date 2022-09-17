using UnityEngine;

public class Sector : MonoBehaviour
{

    //public readonly Material material;
    //public bool IsKiller { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        HelixJampGameLogic.GameState.Game.CurrentLevel.Interaction.OnSectorToPlayerCollision(collision);
    }
    
    
}
