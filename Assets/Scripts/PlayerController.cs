using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;
    public Vector3 rotationSpeed = Vector3.up * 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {

            rb.AddForce(transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(Vector3.Normalize((-transform.right+transform.forward)) * speed);

            Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(Vector3.Normalize((transform.forward + transform.right)) * speed);

            Quaternion deltaRotation = Quaternion.Euler(-rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

}
