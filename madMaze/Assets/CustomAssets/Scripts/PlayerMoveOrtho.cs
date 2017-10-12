using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOrtho : MonoBehaviour
{

    public float speed = 5; //only useful si pas cases
    public float step = .2f; // only useful si cases
    public Quaternion goalRot;
    public Vector3 goalPos;
    [SerializeField]
    private float rotationSpeed = 50;
    private Vector3[] directions = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0) };
    private int dir = 0;
    private bool rotating = false;
    private bool advancing = false;

    public Material[] mats;
    private Color[] baseCols;
    private Color tmpCol;
    private float emit;

    private void Start()
    {
        goalPos = transform.position;
    }

    void SoftRotate(int rot)
    {
        rotating = true;
        dir = (dir + rot + 4) % 4;
        print("dirs number: " + dir + ", dir: " + directions[dir]);
        goalRot = Quaternion.Euler(directions[dir]);
        baseCols = new Color[mats.Length];

        for (int i = 0; i < mats.Length; i++)
        {
            baseCols[i] = mats[i].color;
        }
    }

    void SoftAdvance()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2)) {
            if (hit.collider.gameObject.layer == gameObject.layer || hit.collider.gameObject.layer == 0) {
                return;
            }
        }

        advancing = true;
        goalPos = transform.position + transform.forward*2;
    }

    void Update()
    {
        // follow-up on SoftAdvance()
        if (advancing)
        {
            if (goalPos != transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, goalPos, step);
                if (Vector3.Distance(goalPos, transform.position) <= step * .1)
                {
                    transform.position = goalPos;
                }
            }
            else
            {
                advancing = false;
            }
        }

        if (rotating)
        {
            // follow-up on SoftRotate()
            if (goalRot != transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRot, rotationSpeed * Time.deltaTime);
                foreach (Material mat in mats)
                {

                    /*
                     //change alpha (needs a standard fade shader)
                    tmpCol = mat.color;
                    tmpCol.a = Mathf.Abs(Mathf.Lerp(1, -1, ((transform.rotation.eulerAngles.y + 360) % 90) / 90));
                    mat.color = tmpCol;
                    */

                    //change emit
                    emit = 1 - Mathf.Abs(Mathf.Lerp(1, -1, ((transform.rotation.eulerAngles.y + 360) % 90) / 90));
                    tmpCol = new Color(emit, emit, emit, 1);
                    mat.SetColor("_EmissionColor", tmpCol);
                }
            }

            else
            {
                rotating = false;
                transform.rotation = goalRot;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].SetColor("_EmissionColor", new Color(0, 0, 0, 1));
                    tmpCol = new Color(0, 0, 0, 1);
                    //mats[i].color = baseCols[i];
                }
            }
        }

        if (Input.anyKeyDown)
        {
            if (advancing || rotating)
            {
                transform.position = goalPos;
                transform.rotation = goalRot;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoftAdvance();
            //transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (!rotating)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoftRotate(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SoftRotate(-1);
            }

        }
    }

}
