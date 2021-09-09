using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class loadspecificscene : MonoBehaviour
{

    /* 
    Lorsque le joueur entre dans le collider, il est transporter dans le niveau indiqué
    */
    
    public string Level;

    public void Start()
    {
        AudioManager.instance.Stop("MenuMusic");
        AudioManager.instance.Play("BgMusic");
    }


    private void OnTriggerEnter(Collider collision){
        if(collision.CompareTag("Player")){
           SceneManager.LoadScene(Level);
            
        }
    }

}
