using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    Health PlayerHealth;

    [SerializeField]
    GameObject healthIcon;

    void Start()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        for (int i = 0; i < PlayerHealth.MaxHP; i++)
        {
            Instantiate(healthIcon, transform);
        }
    }

    void LateUpdate()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < PlayerHealth.MaxHP; i++)
        {
            if (i < PlayerHealth.HP)
            {
                Image img = transform.GetChild(i).GetComponent<Image>();
                img.enabled = true;
            }
            else
            {
                Image img = transform.GetChild(i).GetComponent<Image>();
                img.enabled = false;
            }
        }
    }
}
