using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nameScene;

    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditScene()
    {
        SceneManager.LoadScene(nameScene);
    }
}