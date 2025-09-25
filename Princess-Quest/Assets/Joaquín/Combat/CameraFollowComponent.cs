using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset = new Vector3(0, 0, 20);

    void LateUpdate()
    {
        Vector3 targetPos = target.position;

        transform.position = targetPos + offset;
    }
}
