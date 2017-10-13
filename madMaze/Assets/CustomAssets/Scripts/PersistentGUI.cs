using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGUI : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += DestroyDuplicates;
    }

    public void DestroyDuplicates(Scene scene, LoadSceneMode mode)
    {
        GameObject[] guis = GameObject.FindGameObjectsWithTag("GUI");
        foreach (GameObject gui in guis)
        {
            if (gui.name == gameObject.name && gui != gameObject)
            {
                Destroy(gui);
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= DestroyDuplicates;
    }

}
