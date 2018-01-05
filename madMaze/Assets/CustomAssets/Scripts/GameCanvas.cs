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
	private float[] sizeCamera; // = {0, 6, 10, 16, 18}; //for some reason here it doesn't work?
    public Canvas waitingScreen;
    public Image[] waitingImgs;
    [SerializeField]
    private float fadeTime;
	private enum state
	{
		playing,
		lvlOut,
		lvlIn,
	};
	private state currState;
	public GameObject mapHider;
	//int i;



    private void OnEnable()
    {
		sizeCamera = new float[] {0, 6, 10, 14, 16}; // this is the optimal size I measured for each level
        SceneManager.sceneLoaded += CheckIfMenuScene;
        SceneManager.sceneLoaded += InitializeMapCamera;
    }


    public void CheckIfMenuScene(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "menu" || SceneManager.GetActiveScene().name == "endMenu")
        {
            gameObject.GetComponent<Canvas>().enabled = false;
			waitingScreen.enabled = false;
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }


	public void DisableWaitingScreen(Scene scene, LoadSceneMode mode){
		waitingScreen.enabled = false;
	}

    public void ShowMap()
    {
        mapCanvas.SetActive(!mapCanvas.activeSelf);
    }
	public void ShowMap(bool ouinon)
	{
		mapCanvas.SetActive(ouinon);
	}
    
    public void InitializeMapCamera(Scene scene, LoadSceneMode mode)
    {
            GameObject[] singletons = GameObject.FindGameObjectsWithTag("singleton");
            foreach (GameObject single in singletons) {
                if (MapCamera == null && single.name == "MapCamera") {
                    MapCamera = single.GetComponent<Camera>();
				}
				else if (single.name == "mapHider") {
					mapHider = single;
			}
		}
		MapCamera.orthographicSize = sizeCamera[SceneManager.GetActiveScene().buildIndex];
		mapCanvas.SetActive (false);
		for (int i = 0; i <= MapCamera.orthographicSize*2; i++) {
			for (int j = 0; j <= MapCamera.orthographicSize*2; j++) {
				Instantiate (mapHider, new Vector3 (mapHider.transform.position.x + (mapHider.transform.localScale.x * i), mapHider.transform.position.y, mapHider.transform.position.z - (mapHider.transform.localScale.z * j)), Quaternion.identity);
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
			changeScene(SceneManager.GetActiveScene().buildIndex + 1, .2f);
	}

	public void gotoMenu() {
		SceneManager.LoadScene("menu");
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void changeScene (int goalScene, float waitTime)
	{
		StartCoroutine (appear(waitingScreen.GetComponentInChildren<Image>(), waitingScreen.GetComponentInChildren<Text>(), waitTime, .1f, goalScene));
	}

	IEnumerator appear(Image img, Text tx, float wait, float step, int goalScene){
		// checking whether waitingScreen is the right one
		//string[] names = {"TESSSSTTTTTT!","nope","sure", "why not", "this is the fifth level", "maybe?"};
		//string[] names2 = {"truc","chose","bidule", "some", "a few", "some more"};
		//waitingScreen.name = names[i];
		//i++;
		currState = state.lvlOut;
		waitingScreen.enabled = true;
		tx.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1);

		while (currState == state.lvlOut) {
			tx.color = new Color (tx.color.r, tx.color.g, tx.color.b, img.color.a + step);
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + step);
			if (img.color.a >= 1) {
				currState = state.lvlIn;
			}
			yield return new WaitForSeconds (step*wait);
		}
		SceneManager.LoadScene(goalScene);
		//waitingScreen.name = names2[i];
		yield return new WaitForSeconds (wait);

		while (currState == state.lvlIn) {
			tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, img.color.a - step);
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - step);
			if (img.color.a <= 0f) {
				currState = state.playing;
			}
			yield return new WaitForSeconds (step*wait);
		}
		currState = state.playing;
		waitingScreen.enabled = false;
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.P)) {
			gotoNextSceneWithFlair ();
		}
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfMenuScene;
        SceneManager.sceneLoaded -= InitializeMapCamera;

    }
}
