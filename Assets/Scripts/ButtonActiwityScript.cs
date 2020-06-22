using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActiwityScript : MonoBehaviour
{
    [Header("All GameObjects")]
    [SerializeField] private GameObject optionsMenu ;
    [SerializeField] private GameObject mainMenu ;


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void GoToMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GoToOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void onAudio()
    {
        if(AudioManager.canPlay)
        {
            AudioManager.canPlay = false;
        }
        else
        {
            AudioManager.canPlay = true;
        }
    }

    public void ExitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
