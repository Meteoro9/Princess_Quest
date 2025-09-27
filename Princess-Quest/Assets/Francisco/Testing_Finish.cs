using UnityEngine;

public class Testing_Finish : MonoBehaviour
{
    private int enemiesInside = 0;

    [SerializeField] MeshRenderer greenPlane;
    [SerializeField] MeshRenderer winText;

    private void OnEnable()
    {
        greenPlane.enabled = false;
        winText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside++;
            Debug.Log("Se añadió un enemigo a la cantidad" + enemiesInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside--;

            if (enemiesInside < 0) enemiesInside = 0;

            Debug.Log("Se quitó un enemigo de la cantidad");
            //greenPlane.enabled = true;
        }
    }

    private void Update()
    {
        if (enemiesInside <= 0)
        {
            greenPlane.enabled = true;
            winText.enabled = true;
        }
    }
}
