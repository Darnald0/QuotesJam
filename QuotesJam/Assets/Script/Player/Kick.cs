using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class Kick : MonoBehaviour
{
    private float strength = 10.0f;
    private float duration;
    private CameraShake cameraShake;

    private AnimationCurve shakeIntensity;

    private void Awake()
    {
        PlayerController player = gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        strength = player.kickStrength;
        duration = player.shakeDuration;
        shakeIntensity = player.shake;
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnnemyLife ennemy = other.gameObject.GetComponent<EnnemyLife>();
            ennemy.Die(1); 
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidBody != null)
            {
                Debug.Log("touch");  
                StartCoroutine(cameraShake.Shake(duration, shakeIntensity));
                //Vector3 direction = other.transform.position - this.transform.position;
                //direction.y = 0;
                //enemyRigidBody.AddForce(direction.normalized * strength, ForceMode.VelocityChange);
            }
        }

        if (other.tag == "Door")
        {
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidBody != null)
            {
                other.gameObject.GetComponent<Door>().numberOfKick++;
                AudioManager.instance.Play("Destruction");
                StartCoroutine(cameraShake.Shake(duration, shakeIntensity));
                Vector3 direction = other.transform.position - this.transform.position;
                direction.y = 0;
                enemyRigidBody.AddForce(direction.normalized * strength, ForceMode.VelocityChange);
            }
        }
    }
}
