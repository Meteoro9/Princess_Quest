using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; //Referencia al jugador
    [SerializeField] Vector3 offset; //Posición de la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            //Posición de la camara
            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                target.position.z + offset.z
            );

            //Movimiento de la camara
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position, 
                desiredPosition, 1f
            );

            transform.position = smoothedPosition;
        }
    }
}

