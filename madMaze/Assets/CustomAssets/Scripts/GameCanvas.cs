using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    [SerializeField]
    private Camera MapCamera;
    [SerializeField]
    private GameObject mapCanvas;
    [SerializeField]
    private float[] sizeCamera;


    private void OnEnable()
    {
        if (sizeCamera == null) {
            sizeCamera = new float[SceneManager.sceneCountInBuildSettings];
        }
        SceneManager.sceneLoaded += CheckIfMenuScene;
        SceneManager.sceneLoaded += InitializeMapCamera;
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
        MapCamera.orthographicSize = sizeCamera[SceneManager.GetActiveScene().buildIndex];
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfMenuScene;
        SceneManager.sceneLoaded -= InitializeMapCamera;
    }
}
