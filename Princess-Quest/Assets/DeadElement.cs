using UnityEngine;

public class DeadElement : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(10);
        }
    }
}
