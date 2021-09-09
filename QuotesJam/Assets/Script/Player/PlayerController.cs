using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float limiter = 0.7f;
    public float kickStrength = 10.0f;
    [SerializeField] private float kickCoolDown = 0.5f;
    [SerializeField] private float kickDuration = 0.5f;
    public float shakeDuration;
    public AnimationCurve shake;

    public Animator anim;

    private float cd;
    [SerializeField] private Collider kickHitBox;
    private Vector3 movement;
    public Rigidbody rb;
    public CapsuleCollider playerCollider;
    public MeshRenderer meshRenderer;
    public Camera mainCamera;
    private bool isKicking = false;
    private float timeKickEnd;
    public static PlayerController instance;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>();
        Debug.Log(kickHitBox);
        cd = kickCoolDown;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 && movement.z != 0)
        {
            movement.x *= limiter;
            movement.z *= limiter;
        }

        //rb.velocity = new Vector3(movement.x * speed, movement.y, movement.z * speed);
        rb.velocity = new Vector3(movement.x * speed * Time.deltaTime, movement.y, movement.z * speed * Time.deltaTime);

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.z != 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }

        cd -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (cd <= 0)
            {
                isKicking = true;
                AudioManager.instance.Play("KickLeger");
                timeKickEnd = Time.time + kickDuration;
            }
        }

        if (isKicking)
        {
            Kick();
        }

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if(groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }

    private void Kick()
    {
        if (Time.time > timeKickEnd)
        {
            cd = kickCoolDown;
            kickHitBox.enabled = false;
            isKicking = false;
            animator.SetBool("isKicking", false);
        }
        else
        {
            animator.SetBool("isKicking", true);
            kickHitBox.enabled = true;
        }
    }
}
