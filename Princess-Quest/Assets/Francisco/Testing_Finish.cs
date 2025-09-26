using UnityEngine;

public class Testing_Finish : MonoBehaviour
{
    private int enemiesInside = 0;

    MeshRenderer greenPlane;

    private void OnEnable()
    {
        greenPlane = GetComponentInChildren<MeshRenderer>();

        greenPlane.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside++;
            Debug.Log("Se a�adi� un enemigo a la cantidad" + enemiesInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside--;

            if (enemiesInside < 0) enemiesInside = 0;

            Debug.Log("Se quit� un enemigo de la cantidad");
            //greenPlane.enabled = true;
        }
    }

    private void Update()
    {
        if (enemiesInside <= 0)
        {
            greenPlane.enabled = true;
        }
    }
}
