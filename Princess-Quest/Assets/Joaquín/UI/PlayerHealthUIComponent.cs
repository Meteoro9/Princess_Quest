using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    Health targetHealth;

    [SerializeField]
    GameObject healthIcon;

    void Start()
    {
        for (int i = 0; i < targetHealth.MaxHP; i++)
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
        for (int i = 0; i < targetHealth.MaxHP; i++)
        {
            if (i < targetHealth.HP)
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
