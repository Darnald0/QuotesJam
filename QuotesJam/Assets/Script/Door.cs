using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Rigidbody rb;
    public int numberOfKick = 0;
    private BoxCollider col;
    private bool alreadyKicked = false;
    public bool isGettingKick = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (rb.velocity.magnitude <= 0)
        {
            isGettingKick = false;
        }

        if (numberOfKick >= 3)
        {
            col.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnnemyLife ennemy = collision.gameObject.GetComponent<EnnemyLife>();
            ennemy.Die(1);
        }
    }
}
