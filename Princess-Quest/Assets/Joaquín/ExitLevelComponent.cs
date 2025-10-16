using UnityEngine;
using UnityEngine.Events;

public class ExitLevelComponent : MonoBehaviour
{
    bool playerIsIn = false;

    [SerializeField]
    UnityEvent OnLevelExit = new();

    void Update()
    {
        if (playerIsIn && Input.GetKeyDown(KeyCode.W))
        {
            OnLevelExit?.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
        }
    }
#if UNITY_EDITOR

    [SerializeField]
    Color debugBoxColor = Color.yellow;

    void OnDrawGizmos()
    {
        BoxCollider boxColl = GetComponent<BoxCollider>();
        Gizmos.color = debugBoxColor;
        Gizmos.DrawWireCube(transform.position + boxColl.center, boxColl.size);
    }

#endif
}
