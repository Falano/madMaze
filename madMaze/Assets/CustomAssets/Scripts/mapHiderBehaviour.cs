using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapHiderBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
		
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			gameObject.SetActive (false);
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
