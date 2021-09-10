using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;

    public GameObject gameUI;

    public void VictoryCondition() //remettre nom de void pour victoire
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            
        }*/

        gameUI.SetActive(false);
        // condition de victoire = mort de tout les ennemies
        victoryScreen.SetActive(true);
    }
}
