using UnityEngine;

public class FlyingEnemyAnimationHelper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() { }
}
