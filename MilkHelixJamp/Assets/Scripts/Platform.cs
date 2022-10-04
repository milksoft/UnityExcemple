using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameInputControler.Instance.IsPlayer(other.gameObject))
        {
            //if (GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform != null)
            //    Exploid(GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform.gameObject);

            GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform = this.gameObject.transform;
            GameInputControler.Instance.GameState.CurrentLevel.AddScore();
        }
    }

    private void Exploid(GameObject platform)
    {
        const float Volume = 5;
        var center = this.transform.position;
        Debug.Log(this.gameObject.name);
        var rigidbodies = platform.GetComponentsInChildren<Rigidbody>();
        foreach (var item in rigidbodies)
        {
            var position = item.transform.position;
            var toPosition = position - center;
            item.AddForce(toPosition * Volume, ForceMode.Impulse);
        }
    }
}