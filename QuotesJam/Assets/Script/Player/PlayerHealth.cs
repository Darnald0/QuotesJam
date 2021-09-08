
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    /* 
    Gère la santé et la mort du joueur et l'invincibilité en cas de contact avec ennemis sur le joueur
    */
    public int playerLife = 1;
    
    public static PlayerHealth instance;
    // public DeathMenu deathMenu;

    private void Awake(){
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.K)){
           TakeDamage(3);
       }

    }

    
    public void TakeDamage(int damage){
        playerLife -=damage;
 
        if(playerLife <= 0) 
        {
            Die();
            return;
        }
    }

    public void Die(){
        
        
        PlayerController.instance.enabled = false;
        PlayerController.instance.rb.isKinematic = true;
        PlayerController.instance.playerCollider.enabled = false;
        PlayerController.instance.meshRenderer.enabled = false;
        // deathMenu.OnPlayerDeath();
    }
    public void Respawn()
    {
        // Playerbehaviour.instance.enabled = true;
        // Playerbehaviour.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        // Playerbehaviour.instance.playercollider.enabled = true;
        playerLife = 1;
        
    }
}
