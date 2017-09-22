using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matPlayable : MonoBehaviour {
    public Color color = Color.black;
    Renderer[] rends;

	// Use this for initialization
	void Start () {
        rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer rd in rends) {
            rd.material.color = color;
        }
	}
}
