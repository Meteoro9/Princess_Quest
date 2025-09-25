using System.Collections;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPosition;

    [SerializeField]
    float speed;

    [SerializeField]
    float minDistance = 0.01f;

    Rigidbody rb;

    [SerializeField]
    int buttonsNedded;

    [SerializeField]
    int amountOfButtons;
    int AmountOfButtons
    {
        get { return amountOfButtons; }
        set
        {
            amountOfButtons = value;
            if (amountOfButtons >= buttonsNedded)
            {
                MoveToTargetPosition();
            }
        }
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddButton()
    {
        AmountOfButtons++;
    }

    public void RemoveButton()
    {
        AmountOfButtons--;
    }

    public void MoveToTargetPosition()
    {
        StartCoroutine(MovementCoroutine());
    }

    IEnumerator MovementCoroutine()
    {
        while (Vector3.Distance(transform.position, targetPosition) > minDistance)
        {
            Vector3 dir = (targetPosition - transform.position).normalized;

            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
