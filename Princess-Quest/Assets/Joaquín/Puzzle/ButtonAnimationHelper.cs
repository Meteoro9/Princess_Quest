using System;
using System.Collections;
using UnityEngine;

public class ButtonAnimationHelper : MonoBehaviour
{
    [SerializeField]
    ButtonComponent buttonComponent;

    [SerializeField]
    float animSpeed = 1;

    [SerializeField]
    float amountToMoveDown;

    void Start()
    {
        buttonComponent.OnButtonPushed.AddListener(ButtonPushed);
        buttonComponent.OnButtonReleased.AddListener(ButtonReleased);
    }

    void ButtonPushed()
    {
        StartCoroutine(MoveDown());
    }

    void ButtonReleased()
    {
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveDown()
    {
        float finalDestination = transform.position.y - amountToMoveDown;
        // Debug.Log(finalDestination + " final destination");
        // Debug.Log((transform.position.y > finalDestination) + " while statement");
        while (transform.position.y > finalDestination)
        {
            transform.Translate(animSpeed * Time.deltaTime * Vector3.down);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveUp()
    {
        float finalDestination = transform.position.y + amountToMoveDown;
        // Debug.Log(finalDestination + " final destination");
        // Debug.Log((transform.position.y > finalDestination) + " while statement");
        while (transform.position.y < finalDestination)
        {
            transform.Translate(animSpeed * Time.deltaTime * Vector3.up);
            yield return new WaitForEndOfFrame();
        }
    }
}
