using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;

    public void victoryCondition()
    {
        victoryScreen.SetActive(true);
    }
}
