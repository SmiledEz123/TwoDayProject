using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoted : MonoBehaviour
{
    Boolean shot;
    Rigidbody m_RigidBody;
    float mMoveSpeed = 100;
    Vector3 newPoss;
    float cd=0.05f;
    float time;

    Boolean activateCollision = false;
    //float moveSpeed = 15;
    //float mMoveSpeed = 5;
    // Start is called before the first frame update
    
    void Start()
    {
        shot = false;
        m_RigidBody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(shot)
        {
        
        newPoss = transform.position + transform.forward * mMoveSpeed * Time.deltaTime;
        transform.InverseTransformDirection(newPoss);
        m_RigidBody.MovePosition(newPoss);
        if(time<cd)
        {
        time = time + Time.deltaTime;
        }
        else
        {if(activateCollision == false)
        {
            activateCollision = true;
        }
        }
        }
        }
    public void Shot()
    {
        shot = true;
    }

   private void OnTriggerEnter(Collider other) {
    if(activateCollision)
    {
        if(other.gameObject.tag == "Rat")
    {
        other.GetComponent<RatBehavior>().Die();
    }
        Destroy(gameObject);
    }
   }
}
    