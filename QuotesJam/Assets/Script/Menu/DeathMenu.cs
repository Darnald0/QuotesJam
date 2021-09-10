using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;

    /*public void OnPlayerDeath()
    {
        DeathScreen.SetActive(true);
    }*/

    public void RetryButton()
    {
        //PlayerHealth.instance.Respawn();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DeathScreen.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.Play("BgMusic");
        AudioManager.instance.Stop("MenuMusic");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
