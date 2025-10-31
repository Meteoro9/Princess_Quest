using UnityEngine;

public class PauseHelper : MonoBehaviour
{
    [SerializeField]
    MenuFunctions menuFunctions;

    [SerializeField]
    GameObject pauseScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            menuFunctions.AddToUI(pauseScreen);
            menuFunctions.PauseGame();
        }
    }
}
