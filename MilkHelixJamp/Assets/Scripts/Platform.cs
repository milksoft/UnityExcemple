using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        
        //if (other.TryGetComponent(out Player player))
        //{
            HelixJampGameLogic.GameState.Game.CurrentLevel.Interaction.OnNewStep(this.gameObject,other);
            //this.gameObject.GetComponentInParent<LevelInit>().CurrentPlatform = this;
            //player.CurrentPlatform = this;
        //}
    }
}