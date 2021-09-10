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

    public Score score;

    private Animator animator;
    private bool isDead = false;
   
   public void Awake()
   {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<BoxCollider>();
        //meleeCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        enemyDetector = GetComponent<EnemyDetector>();
        animator = GetComponent<Animator>();
   }
   public void Die(int damage)
   {
       life -= damage;

       if(life <= 0 && !isDead)
       {
            animator.SetBool("isDying", true);
            isDead = true;
            Debug.Log("EneMort");
            AudioManager.instance.Play("HitLeger");
            //enemyDetector.enabled = false;
            //rb.isKinematic = true;
            //enemyCollider.enabled = false;
            //meleeCollider.enabled = false;
            //mesh.enabled = false;
            StopCoroutine(score.Combo());
            StartCoroutine(score.Combo());
            score.scoreValue += 10 * score.multiplier;
            //Destroy(gameObject);
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            enemyDetector.enabled = false;
            enemyCollider.enabled = false;
            meleeCollider.enabled = false;

        }
   }
    
}