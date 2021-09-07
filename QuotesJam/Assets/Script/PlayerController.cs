using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float limiter = 0.7f;
    private Vector3 movement;
    private Vector3 mousePos;
    private Rigidbody rb;
    public Camera mainCamera;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
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
        
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if(groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }
}
