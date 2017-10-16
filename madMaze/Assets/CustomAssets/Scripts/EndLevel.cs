using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
    menuManager menu;

	// Use this for initialization
	void Start () {
        menu = GameObject.FindGameObjectWithTag("GameController").GetComponent<menuManager>();
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            menu.gotoNextSceneWithFlair();
        }
    }

}
