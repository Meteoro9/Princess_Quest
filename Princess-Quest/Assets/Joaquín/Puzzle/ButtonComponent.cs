using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ButtonComponent : MonoBehaviour
{
    public UnityEvent OnButtonPushed;

    public UnityEvent OnButtonReleased;

    List<GameObject> objectsPressing = new();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            objectsPressing.Add(other.gameObject);
            if (objectsPressing.Count == 1)
            {
                OnButtonPushed?.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            objectsPressing.Remove(other.gameObject);
            if (objectsPressing.Count == 0)
            {
                OnButtonReleased?.Invoke();
            }
        }
    }
}
