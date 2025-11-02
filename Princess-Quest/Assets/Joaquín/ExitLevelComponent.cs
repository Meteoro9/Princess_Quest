using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ExitLevelComponent : MonoBehaviour
{
    bool playerIsIn = false;

    [SerializeField]
    UnityEvent OnLevelExit = new();

    [SerializeField]
    UnityEvent OnPlayerEnterArea = new();

    [SerializeField]
    UnityEvent OnPlayerLeftArea = new();

    void Update()
    {
        if (playerIsIn && UpKeyPressed())
        {
            OnLevelExit?.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
            OnPlayerEnterArea?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
            OnPlayerLeftArea?.Invoke();
        }
    }

    bool UpKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }
#if UNITY_EDITOR

#endif
}
