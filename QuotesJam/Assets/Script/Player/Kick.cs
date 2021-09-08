using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private float strength = 10.0f;
    private int damage = 1;
    public EnnemyLife ennemyLife;

    private void Awake()
    {
        PlayerController player = gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        ennemyLife = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnnemyLife>();
        strength = player.kickStrength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("touch");  
            ennemyLife.Die(damage);
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidBody != null)
            {
                Vector3 direction = other.transform.position - this.transform.position;
                direction.y = 0;
                enemyRigidBody.AddForce(direction.normalized * strength, ForceMode.VelocityChange);
            }
        }
    }
}
