using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            CalculateFiringSolution fs = new CalculateFiringSolution();
            Nullable<Vector3> aimVector = fs.AngleForTargeting(transform.position, target.transform.position, launchForce, Physics.gravity);
            if (aimVector.HasValue)
            {
                rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
            }
        }
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = startPos;
            rb.isKinematic = false;
        }
    }
}
