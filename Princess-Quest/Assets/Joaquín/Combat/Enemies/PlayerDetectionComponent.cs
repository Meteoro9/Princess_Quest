using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetectionComponent : MonoBehaviour
{
    [SerializeField]
    bool alertEnemiesOnPlayerEntered = true;

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

        /*       Collider[] colliders = Physics.OverlapBox(transform.position, boxColl.size / 2);
                foreach (Collider coll in colliders)
                {
                    if (coll.CompareTag("Enemy"))
                    {
                        enemiesInArea.Add(coll.gameObject);
                    }
                } */

        foreach (Transform tran in transform)
        {
            enemiesInArea.Add(tran.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEntered?.Invoke();
            if (alertEnemiesOnPlayerEntered)
            {
                foreach (GameObject enemy in enemiesInArea)
                {
                    EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                    enemyMovement.SetTarget(collision.gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLeft?.Invoke();
        }
    }

    public void RemoveFromEnemyList(GameObject enemy)
    {
        enemiesInArea.Remove(enemy);
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
