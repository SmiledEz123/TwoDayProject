using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float moveSpeed = 50;
    float mMoveSpeed = 5;
    float horizontalInput;
    float verticalInput;
    Rigidbody m_RigidBody;
    float cd=2f;
    float time;
    [SerializeField] Transform fleche;
    Transform fleche2;
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        time=0;
    }

    void Update()
    {
        Vector3 mouseMovement;
        mouseMovement = mMoveSpeed * new Vector3(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0);
        horizontalInput = getXaxis();
        verticalInput = getYaxis();
        Vector3 m_Input = new Vector3(horizontalInput,0,verticalInput)* (Time.deltaTime * moveSpeed);
        m_Input = transform.TransformDirection(m_Input);
        Vector3 newPoss = transform.position + m_Input;
        m_RigidBody.MovePosition(newPoss);
        transform.eulerAngles += mouseMovement;
        transform.InverseTransformDirection(mouseMovement);
        if(time<cd)
        {
        time = time + Time.deltaTime;
        }
        else
        {
            showBullet();
            if(Input.GetButton("Fire1"))
        {
           shoot();
           fleche.transform.parent = null;
           fleche = Instantiate(fleche,fleche.transform.position,fleche.rotation);
           fleche.parent = gameObject.GetComponent<Transform>();
        }
        }
        
    }



    float getXaxis()
    {
        return Input.GetAxis("Horizontal");
    }

    float getYaxis()
    {
        return Input.GetAxis("Vertical");
    }


    private void shoot()
    {
        
        if(time > cd)
        {
            time=0;
            fleche.GetComponent<Shoted>().Shot();
        }
    }

    private void showBullet()
    {
        fleche.gameObject.SetActive(true);
    }

    public void TakeDmg()
    {
        Debug.Log("outch");
    }
}
