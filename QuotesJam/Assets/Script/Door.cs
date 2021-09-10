using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Rigidbody rb;
    public bool isGettingKick = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (rb.velocity.magnitude <= 0)
        {
            isGettingKick = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && isGettingKick)
        {
            EnnemyLife ennemy = collision.gameObject.GetComponent<EnnemyLife>();
            ennemy.Die(1);
        }
    }
}
