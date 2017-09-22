using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orientationPlayerOrtho : MonoBehaviour {
    private string orientation = "north";
    private GameObject go;

	// Use this for initialization
	void Start () {
        go = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.eulerAngles.y < 45 || transform.eulerAngles.y >= 315) {
             orientation = "north";
        }
        if (transform.eulerAngles.y >= 45 && transform.eulerAngles.y < 135)
        {
            orientation = "east";
        }
        if (transform.eulerAngles.y >= 135 && transform.eulerAngles.y < 225)
        {
            orientation = "south";
        }
        if (transform.eulerAngles.y >= 225 && transform.eulerAngles.y < 315)
        {
            orientation = "west";
        }
        
        if (go.layer!= LayerMask.NameToLayer(orientation)) {
            changeLayerOrtho.changerOrtho.ChangeLayer(orientation);
                }
    }
}
