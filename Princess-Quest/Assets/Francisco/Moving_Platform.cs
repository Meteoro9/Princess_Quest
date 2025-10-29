using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    public bool isMoving { get; private set; } = false;
    

    public void SetIsMoving(bool _isMoving)
    {
        isMoving = _isMoving;
    }
}
