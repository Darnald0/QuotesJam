using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private float strength = 10.0f;
    private Vector3 dir;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("touch");
            dir = this.transform.rotation * Vector3.forward;
            
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidBody != null)
            {
                Vector3 direction = other.transform.position - this.transform.position;
                direction.y = 0;
                enemyRigidBody.AddForce(direction.normalized * strength, ForceMode.Impulse);
            }
        }
    }
}
