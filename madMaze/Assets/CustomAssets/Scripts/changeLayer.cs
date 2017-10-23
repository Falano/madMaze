using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLayer : MonoBehaviour {
    GameObject player;
    Camera camera;
    public static changeLayer changer;


    void Awake()
    {
        if (changer == null)
        {
            //DontDestroyOnLoad(gameObject);
            changer = this;
        }
        else if (changer != this)
        {
            Destroy(gameObject);
        }
    }

        void Start () {
        if (SceneManager.GetActiveScene().name != "menu" && SceneManager.GetActiveScene().name != "endMenu")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            camera = player.GetComponentInChildren<Camera>();
        }
    }


    public void ChangeLayer(string layer) {
        player.layer = LayerMask.NameToLayer(layer);
        print("(p)layer = " + layer);
        camera.cullingMask = (1 << player.layer) | (1 << 0);
    }

    void ChangeLayer(string layer, GameObject obj)
    {
        obj.layer = LayerMask.NameToLayer(layer);
        print(obj.name + " layer = " + layer);
        camera.cullingMask = (1 << obj.layer) | (1 << 0);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Y)) {
            ChangeLayer("north");
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            ChangeLayer("east");

        }
        if (Input.GetKeyDown(KeyCode.H)) {
            ChangeLayer("south");

        }
        if (Input.GetKeyDown(KeyCode.G)) {
            ChangeLayer("west");

        }
    }
}
