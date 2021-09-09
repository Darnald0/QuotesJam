using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target)
        {
            transform.position = new Vector3(
            target.transform.position.x + offset.x,
            target.transform.position.y + offset.y, 
            target.transform.position.z + offset.z);
        }
    }
}
