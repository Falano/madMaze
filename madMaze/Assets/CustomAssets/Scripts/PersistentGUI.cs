using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGUI : MonoBehaviour {
    public static GameObject unDestroyable;



    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
        gameObject.tag = "singleton";
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += DestroyDuplicates;
    }

    public void DestroyDuplicates(Scene scene, LoadSceneMode mode)
    {
        if (unDestroyable == null)
        {
            unDestroyable = gameObject;
        }
        else if (unDestroyable!= gameObject)
        {
            Destroy(gameObject);
        }
    }

    /*
    public void DestroyDuplicates(Scene scene, LoadSceneMode mode)
    {
        print("duplicates destroyed");
        GameObject[] singletons = GameObject.FindGameObjectsWithTag("singleton");
        foreach (GameObject single in singletons)
        {
            print("current obj: " + single.name);
            if (single.name == gameObject.name && single != gameObject)
            {
                print(single.name + "DESTROYED");
                Destroy(single);
            }
        }
    }
    */

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= DestroyDuplicates;
    }

}
