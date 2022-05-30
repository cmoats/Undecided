using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject boat;
    public float yOffset = 5;
    public float offset = 3;
    public float rotationSpeed = 10;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(boat.transform.position.x - offset, boat.transform.position.y + yOffset, boat.transform.position.z -offset);
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(boat.transform.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(boat.transform.position, boat.transform.up, rotationSpeed * Time.deltaTime);
        }
    }
}
