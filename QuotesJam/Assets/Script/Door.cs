using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnnemyLife ennemy = collision.gameObject.GetComponent<EnnemyLife>();
            ennemy.Die(1);
        }
    }
}
