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
            //Si no hay menu de pausa o fue destruido crea uno
            if (currentPauseMenu == null || !currentPauseMenu)
            {
                Transform uiParent = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0);
                currentPauseMenu = Instantiate(pauseScreen, uiParent);
                menuFunctions.PauseGame();
            }
            else
            {
                //Si hay menu de pausa lo destruye
                Destroy(currentPauseMenu);
                menuFunctions.ResumeGame();
            }
        }
    }
}
