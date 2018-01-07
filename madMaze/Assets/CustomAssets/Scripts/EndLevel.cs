using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
	GameCanvas canvas;

	// Use this for initialization
	void Start () {
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("singleton")) {
			if (obj.name == "GameCanvas") {
				if (obj.GetComponent<GameCanvas> ()) {

					canvas = obj.GetComponent<GameCanvas> ();
				} else {
					Debug.LogError ("Nope: error: no GameCanvas component on the object GameCanvas; won't be able to change level");
				}
			}
		}
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvas.gotoNextSceneWithFlair();
        }
    }

}
