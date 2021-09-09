using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectionMelee : MonoBehaviour
{
    public bool detectionPlayer = false;
    public int damage = 1;
    private DateTime nextDamage;
    public float fightAfterTime;
    void Start()
    {
        nextDamage = DateTime.Now;
    }

    
    void FixedUpdate()
    {
        if(detectionPlayer == true)
        {
            FightInDetection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            detectionPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            detectionPlayer = false;
        }
    }

    public void FightInDetection()
    {
        if(nextDamage <= DateTime.Now)
        {
            Debug.Log("Touché");
            PlayerHealth.instance.TakeDamage(damage);
            nextDamage = DateTime.Now.AddSeconds(System.Convert.ToDouble(fightAfterTime));
        }
    }
}
