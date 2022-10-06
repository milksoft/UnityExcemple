using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameInputControler.Instance.IsPlayer(other.gameObject))
        {
            if (GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform != null)
                ExploidPlatform(GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform.gameObject);

            GameInputControler.Instance.GameState.CurrentLevel.CurrentPlatform = this.gameObject.transform;
            GameInputControler.Instance.GameState.CurrentLevel.AddScore();
        }
        else if (other.TryGetComponent(out Sector s) && s.gameObject.transform.parent != this.gameObject.transform)
        {
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }

    private void ExploidPlatform(GameObject platform)
    {
        Vector3 volume = new Vector3(-10, -10, -10);
        foreach (Transform item in platform.transform)
        {
            if (item.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddTorque(volume, ForceMode.Impulse);
            }
            if (item.gameObject.TryGetComponent(out Sector s))
            {
                s.enabled = false;
            }
            //if (item.TryGetComponent<MeshCollider>(out MeshCollider mc))
            //{
            //    //mc.enabled = false;
            //}
        }
    }
}