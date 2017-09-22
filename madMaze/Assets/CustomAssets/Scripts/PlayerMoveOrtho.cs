using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOrtho : MonoBehaviour
{

    public float speed = 5;
    public Quaternion goalRot;
    [SerializeField]
    private float rotationSpeed = 50;
    private Vector3[] directions = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0) };
    private int dir = 0;
    private bool rotating = false;

    void SoftRotate(int rot)
    {
        rotating = true;
        dir = (dir + rot + 4) % 4;
        print("dirs number: " + dir + ", dir: " + directions[dir]);
        goalRot = Quaternion.Euler(directions[dir]);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("I'm here for you");
            transform.rotation = goalRot;
        }
        if (!rotating)
        {

            if (Input.GetKeyDown(KeyCode.O))
            {
                print("I'm never giving up");
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoftRotate(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SoftRotate(-1);
            }
        }

        // follow-up on SoftRotate()
        if (goalRot != transform.rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRot, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rotating = false;
            transform.rotation = goalRot;
        }
    }
}
