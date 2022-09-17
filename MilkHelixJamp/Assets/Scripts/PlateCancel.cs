using UnityEngine;

public class PlateCancel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            Debug.Log("level up");
        }
    }
}