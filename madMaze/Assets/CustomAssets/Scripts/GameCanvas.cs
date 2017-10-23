using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField]
    private Camera MapCamera;
    [SerializeField]
    private GameObject mapCanvas;
    [SerializeField]
    private float[] sizeCamera; //6 10 16 18
    public Canvas waitingScreen;
    public Image[] waitingImgs;
    [SerializeField]
    private float fadeTime;

    private void OnEnable()
    {
        if (sizeCamera == null) {
        sizeCamera = null;
            sizeCamera = new float[SceneManager.sceneCountInBuildSettings];
        }
        SceneManager.sceneLoaded += CheckIfMenuScene;
        SceneManager.sceneLoaded += InitializeMapCamera;
        SceneManager.sceneLoaded += DisableWaitingScreen;
    }


    public void CheckIfMenuScene(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "menu" || SceneManager.GetActiveScene().name == "endMenu")
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }


    public void ShowMap()
    {
        mapCanvas.SetActive(!mapCanvas.activeSelf);
    }

    
    public void InitializeMapCamera(Scene scene, LoadSceneMode mode)
    {
        if (MapCamera == null) {
            GameObject[] singletons = GameObject.FindGameObjectsWithTag("singleton");
            foreach (GameObject single in singletons) {
                if (single.name == "MapCamera") {
                    MapCamera = single.GetComponent<Camera>();
                }
            }

        }

        MapCamera.orthographicSize = sizeCamera[SceneManager.GetActiveScene().buildIndex];
    }

    public void DisableWaitingScreen(Scene scene, LoadSceneMode mode) {
        waitingScreen.enabled = true;
        /*
        waitingImgs = waitingScreen.GetComponentsInChildren<Image>();
        float alpha = 1;
        float time = Time.time;
        while (alpha > 0)
        {
            alpha = 1-((Time.time - time) / fadeTime);
            foreach (Image img in waitingImgs)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            }
        }
        */
        waitingScreen.enabled = false;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfMenuScene;
        SceneManager.sceneLoaded -= InitializeMapCamera;
        SceneManager.sceneLoaded -= DisableWaitingScreen;

    }
}
