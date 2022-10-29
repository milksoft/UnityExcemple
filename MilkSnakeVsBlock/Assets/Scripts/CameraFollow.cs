using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z - 2;
        transform.position = transformPosition;
    }
}