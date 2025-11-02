using UnityEngine;

public class PauseHelper : MonoBehaviour
{
    [SerializeField]
    MenuFunctions menuFunctions;

    [SerializeField]
    GameObject pauseScreen;

    private GameObject currentPauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si no hay menú de pausa o fue destruido, crear uno
            if (currentPauseMenu == null || !currentPauseMenu)
            {
                Transform uiParent = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0);
                currentPauseMenu = Instantiate(pauseScreen, uiParent);
                menuFunctions.PauseGame();
            }
            else
            {
                // Si hay menú de pausa, destruirlo
                Destroy(currentPauseMenu);
                menuFunctions.ResumeGame();
            }
        }
    }
}
