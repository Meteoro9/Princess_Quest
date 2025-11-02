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

    Vector3 originalPos;

    [SerializeField]
    bool MoveBack;

    [SerializeField]
    float waitTime;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    public void AddButton()
    {
        AmountOfButtons++;
    }

    public void RemoveButton()
    {
        AmountOfButtons--;
    }

    [ContextMenu("MoveToTargetPosition")]
    public void MoveToTargetPosition()
    {
        StartCoroutine(MovementCoroutine());
    }

    public void ReturnToOrignialPosition()
    {
        StartCoroutine(ReturnCor());
    }

    IEnumerator ReturnCor()
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        while (Vector3.Distance(transform.position, originalPos) > minDistance)
        {
            Vector3 dir = (originalPos - transform.position).normalized;

            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        if (MoveBack)
        {
            MoveToTargetPosition();
        }
    }

    IEnumerator MovementCoroutine()
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        while (Vector3.Distance(transform.position, targetPosition) > minDistance)
        {
            Vector3 dir = (targetPosition - transform.position).normalized;

            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        if (MoveBack)
        {
            ReturnToOrignialPosition();
        }
    }
}
