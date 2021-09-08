using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float limiter = 0.7f;
    public float kickStrength = 10.0f;
    [SerializeField] private float kickCoolDown = 1.0f;
    [SerializeField] private float kickDuration = 0.5f;

    private float cd;
    private Collider kickHitBox;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isKicking = false;
    private float timeKickEnd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        kickHitBox = transform.GetChild(0).GetComponent<BoxCollider>();
        Debug.Log(kickHitBox);
        cd = kickCoolDown;
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

        cd -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (cd <= 0)
            {
                isKicking = true;

                timeKickEnd = Time.time + kickDuration;
            }
        }

        if (isKicking)
        {
            Kick();
        }
    }

    private void Kick()
    {
        if (Time.time > timeKickEnd)
        {
            cd = kickCoolDown;
            kickHitBox.enabled = false;
            isKicking = false;
        } else
        {
            kickHitBox.enabled = true;
        }
    }
}
