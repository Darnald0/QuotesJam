using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetector : MonoBehaviour
{
    private Ray enemyRay;
    public Color rayColor;
    private RaycastHit raycastHit;
    private bool follow;
    public float speed = 0;

    public NavMeshAgent agent;
    public GameObject target;
    public LayerMask player;
    public Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = LayerMask.GetMask("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        enemyRay = new Ray(transform.position, transform.forward*10);
        Debug.DrawRay(transform.position, transform.forward*10, rayColor);

        if(Physics.Raycast(transform.position, transform.forward, 10, player)) 
        {
            follow = true;
        }
       

        if(follow == true) //&& Vector3.Distance(transform.position, target.transform.position) >= 1
        {
        
            agent.SetDestination(target.transform.position);

            if(PlayerHealth.instance.playerLife <= 0)
            {
                follow = false;
            }
        }
        // else 
        // {
        //     // rb.velocity = Vector3.zero;
        //     // rb.angularVelocity = Vector3.zero;
        //     agent.SetDestination(transform.position);
        // }

        

        
    }

    
}
