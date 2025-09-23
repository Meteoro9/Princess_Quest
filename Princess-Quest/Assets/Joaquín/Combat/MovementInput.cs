using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MovementInput : MonoBehaviour
{
    Movement movement;

    void OnEnable()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        Vector3 inputVec = new(inputX, 0);

        movement.MoveInDirection(inputVec);
    }
}
