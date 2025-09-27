using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetectionComponent : MonoBehaviour
{
    [SerializeField]
    UnityEvent PlayerEntered = new();

    [SerializeField]
    UnityEvent PlayerLeft = new();

    [SerializeField]
    List<GameObject> enemiesInArea = new();

    void OnEnable()
    {
        BoxCollider boxColl = GetComponent<BoxCollider>();
        boxColl.isTrigger = true;

        Collider[] colliders = Physics.OverlapBox(transform.position, boxColl.size / 2);
        foreach (Collider coll in colliders)
        {
            if (coll.CompareTag("Enemy"))
            {
                enemiesInArea.Add(coll.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEntered?.Invoke();
            foreach (GameObject enemy in enemiesInArea)
            {
                enemy.GetComponent<EnemyMovement>().SetTarget(collision.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            PlayerLeft?.Invoke();
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        BoxCollider boxColl = GetComponent<BoxCollider>();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxColl.size);
    }

#endif
}
