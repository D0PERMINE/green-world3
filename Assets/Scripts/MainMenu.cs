using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playerMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        playerMenu.SetActive(false);
    }

    public void RestartGame()
    {
        LoadSceneByName("Level_01");
    }

    public void BackToMainMenu()
    {
        LoadSceneByName("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }

    // Diese Methode lädt die Szene mit dem angegebenen Namen
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
