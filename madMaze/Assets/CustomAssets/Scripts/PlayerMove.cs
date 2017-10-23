using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float rotationSpeed = 5;
    public Vector3 posOffset;
    public float speed = 5;
    public Rigidbody rb;


    private GameObject worldCamera;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, speed * 0.01f);
            changeLayer.changer.ChangeLayer("north");

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, speed * -0.01f);
            changeLayer.changer.ChangeLayer("south");
        }

            if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotationSpeed, 0);
            changeLayer.changer.ChangeLayer("east");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotationSpeed, 0);
            changeLayer.changer.ChangeLayer("west");

        }


    }
}
