using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;

    public void victoryCondition()
    {
        // condition de victoire = mort de tout les ennemies
        victoryScreen.SetActive(true);
    }
}
