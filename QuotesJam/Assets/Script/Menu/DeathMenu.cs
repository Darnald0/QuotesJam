using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;

    public void OnPlayerDeath()
    {
        DeathScreen.SetActive(true);
    }

    public void RetryButton()
    {
        //PlayerHealth.instance.Respawn();
        DeathScreen.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
