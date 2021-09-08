using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float limiter = 0.7f;
    private Vector3 movement;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 && movement.z != 0)
        {
            movement.x *= limiter;
            movement.z *= limiter;

        }

        rb.velocity = new Vector3(movement.x * speed, movement.y, movement.z * speed);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }
}
