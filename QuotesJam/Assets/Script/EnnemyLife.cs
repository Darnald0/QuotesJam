using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public int life = 1;
    public Rigidbody rb;
    public BoxCollider enemyCollider;
    public BoxCollider meleeCollider;
    public MeshRenderer meshRenderer;
    public EnemyDetector enemyDetector;
   
   public void Awake()
   {
    //    rb = GetComponent<Rigidbody>();
    //    enemyCollider = GetComponent<BoxCollider>();
    //    meleeCollider = transform.GetChild(0).GetComponent<BoxCollider>();
    //    meshRenderer = GetComponent<MeshRenderer>();
    //    enemyDetector = GetComponent<EnemyDetector>();
   }
   public void Die(int damage)
   {
       life -= damage;

       if(life <= 0)
       {
            Debug.Log("EneMort");
            SoundManager.Instance.PlaySFX("HitLegers");
            enemyDetector.enabled = false;
            rb.isKinematic = true;
            enemyCollider.enabled = false;
            meleeCollider.enabled = false;
            meshRenderer.enabled = false;
       }
   }
}
