using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLayerOrtho : MonoBehaviour {
    GameObject player;
    Camera camera;
    public static changeLayerOrtho changerOrtho;


    void Awake()
    {
        if (changerOrtho == null)
        {
            DontDestroyOnLoad(gameObject);
            changerOrtho = this;
        }
        else if (changerOrtho != this)
        {
            Destroy(gameObject);
        }
    }


    void OnEnable () {
        SceneManager.sceneLoaded += getPlayerECam;
    }

    void getPlayerECam(Scene scene, LoadSceneMode mode) {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = player.GetComponentInChildren<Camera>();
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

    void OnDisable()
    {
        SceneManager.sceneLoaded += getPlayerECam;
    }
}
