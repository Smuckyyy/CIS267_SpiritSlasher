using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene("GameManager");
        SceneManager.LoadScene("Tutorial");
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene("GameManager");
        SceneManager.LoadScene("Tutorial");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
