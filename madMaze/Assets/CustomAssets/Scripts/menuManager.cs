using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {

    public Canvas waitingScreen;
    public Image[] waitingImgs;
    [SerializeField]
    private float fadeTime = 2;

    public void gotoNextScene()
    {
        // only works because I've got an "end game" scene; otherwise there'd be an "index out of range" error
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void gotoNextSceneWithFlair() { 
        // only works because I've got an "end game" scene; otherwise there'd be an "index out of range" error
        IEnumerator changeSceneNow = changeScene(SceneManager.GetActiveScene().buildIndex + 1, .2f);
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
        waitingScreen.enabled = true;
        /*
        waitingImgs = waitingScreen.GetComponentsInChildren<Image>();
        float alpha = 0;
        float time = Time.time;
        while (alpha <= 1)
        {
            alpha = (Time.time - time)/fadeTime;
            foreach (Image img in waitingImgs)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            }
        }
        */
        yield return new WaitForSeconds(waitTime);
        //waitingScreen.enabled = false;
        SceneManager.LoadScene(goalScene);
    }

}
