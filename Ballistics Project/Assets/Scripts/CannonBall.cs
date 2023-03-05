using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CannonBall : MonoBehaviour
{
    public GameObject target;
    public float launchForce = 10f;
    Rigidbody rb;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void Kick(float kickForce)
    {
        CalculateFiringSolution fs = new CalculateFiringSolution();
        Nullable<Vector3> aimVector = fs.AngleForTargeting(transform.position, target.transform.position, launchForce, Physics.gravity);
        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * kickForce, ForceMode.VelocityChange);
        }
        else
        {
            Kick(kickForce * 2);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            print("space");
            Kick(launchForce);
        }
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = startPos;
            rb.isKinematic = false;
            //SceneManager.LoadScene(0);
        }
    }
}
