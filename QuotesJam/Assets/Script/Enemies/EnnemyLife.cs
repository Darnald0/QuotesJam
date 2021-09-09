using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public int life = 1;
    public Rigidbody rb;
    public BoxCollider enemyCollider;
    public BoxCollider meleeCollider;
    public EnemyDetector enemyDetector;

    public SkinnedMeshRenderer mesh;

    public GameObject score;
   
   public void Awake()
   {
        score = GameObject.FindGameObjectWithTag("Score");
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<BoxCollider>();
        meleeCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        enemyDetector = GetComponent<EnemyDetector>();
   }
   public void Die(int damage)
   {
       life -= damage;

       if(life <= 0)
       {
            Debug.Log("EneMort");
            AudioManager.instance.Play("HitLeger");
            enemyDetector.enabled = false;
            rb.isKinematic = true;
            enemyCollider.enabled = false;
            meleeCollider.enabled = false;
            mesh.enabled = false;
            StopCoroutine(score.Combo());
            StartCoroutine(score.Combo());
            score.scoreValue += 10 * score.multiplier;
        }
   }
    
}
