using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // load the main game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // quit the application (Debug.Log for editor testing)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}