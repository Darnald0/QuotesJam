using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public int life = 1;
   
   public void Die(int damage)
   {
       life -= damage;

       if(life <= 0)
       {
           Destroy(gameObject);
       }
   }
}
