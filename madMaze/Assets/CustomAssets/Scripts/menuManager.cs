using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {

    public GameObject waitingScreen;


    public void InitializewaitingScreen(Scene scene, LoadSceneMode mode)
    {
        if (waitingScreen == null)
        {
            GameObject[] singletons = GameObject.FindGameObjectsWithTag("singleton");
            foreach (GameObject single in singletons)
            {
                if (single.name == "waitingScreen")
                {
                    waitingScreen= single;
                }
            }

        }
    }


    public void gotoNextScene()
    {
        // only works because I've got an "end game" scene; otherwise there'd be an "index out of range" error
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void gotoNextSceneWithFlair() { 
        // only works because I've got an "end game" scene; otherwise there'd be an "index out of range" error
        IEnumerator changeSceneNow = changeScene(SceneManager.GetActiveScene().buildIndex + 1, 1f);
        StartCoroutine(changeSceneNow);
}

public void gotoMenu() {
        SceneManager.LoadScene("menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator changeScene (int goalScene, float waitTime)
    {
        //waitingScreen.SetActive(true);
        waitingScreen.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        //waitingScreen.GetComponent<Canvas>().enabled = false;
        //waitingScreen.SetActive(false);
        SceneManager.LoadScene(goalScene);
    }

}
